import { Component, Inject, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { ScheduleService } from '../../services/schedule.service';
import { MarkerDto } from '../../models/schedule-models';

export interface EventData {
  id?: string;
  name: string;
  startTime: string;
  endTime: string;
  tags: string;
}

@Component({
  selector: 'app-event-edit-form',
  templateUrl: './event-edit-form.component.html',
  styleUrls: ['./event-edit-form.component.scss']
})
export class EventEditFormComponent implements OnInit {
  eventForm: FormGroup;
  tagInput!: ElementRef<HTMLInputElement>;

  @ViewChild('tagInput') set tagInputRef(ref: ElementRef<HTMLInputElement>) {
    if (ref) {
      this.tagInput = ref;
    }
  };

  availableTags: string[] = [];
  selectedTags: string[] = [];
  filteredTags!: Observable<string[]>;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  tagControl = new FormControl('');

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EventEditFormComponent>,
    private scheduleService: ScheduleService,
    @Inject(MAT_DIALOG_DATA) public data: EventData & { spaceId?: number }
  ) {
    this.eventForm = this.createForm();
    this.filteredTags = this.tagControl.valueChanges.pipe(
      startWith(''),
      map((value: string | null) => value ? this._filter(value) : this.availableTags.slice())
    );
  }

  ngOnInit(): void {
    // Load markers/tags dynamically from backend if spaceId provided
    if (this.data.spaceId) {
      this.scheduleService.getMarkersBySpace(this.data.spaceId).subscribe({
        next: (markers: MarkerDto[]) => {
          this.availableTags = markers.map(m => m.name);
          this.filteredTags = this.tagControl.valueChanges.pipe(
            startWith(''),
            map(value => value ? this._filter(value) : this.availableTags.slice())
          );
        },
        error: (err) => {
          console.error('Failed to load markers', err);
          // fallback: use empty or predefined list if needed
          this.availableTags = [];
        }
      });
    }

    if (this.data) {
      // Initialize selectedTags from incoming tags string (comma separated)
      this.selectedTags = this.data.tags ? this.data.tags.split(',').map(t => t.trim()).filter(t => t) : [];

      this.eventForm.patchValue({
        name: this.data.name || '',
        startTime: this.data.startTime || '',
        endTime: this.data.endTime || '',
        tags: this.selectedTags.join(', ')  // show tags as string
      });
    }
  }

  private createForm(): FormGroup {
    return this.fb.group({
      name: ['', [Validators.required]],
      startTime: ['', [Validators.required]],
      endTime: ['', [Validators.required]],
      tags: ['']
    }, { validators: this.timeOrderValidator });
  }

  private timeOrderValidator(group: FormGroup) {
    const startTime = group.get('startTime')?.value;
    const endTime = group.get('endTime')?.value;

    if (startTime && endTime && startTime >= endTime) {
      return { timeOrder: true };
    }
    return null;
  }

  // Wrapper method to safely check errors on form controls (fixes TS errors)
  hasError(controlName: string, errorCode: string): boolean {
    const control = this.eventForm.get(controlName);
    return control ? control.hasError(errorCode) : false;
  }

  onSave(): void {
    if (this.eventForm.valid) {
      const formValue = this.eventForm.value;
      // Return tags as CSV string built from selectedTags, not formControl (which may lag)
      this.dialogRef.close({
        ...this.data,
        ...formValue,
        tags: this.selectedTags.join(', ')
      });
    } else {
      this.eventForm.markAllAsTouched();
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  addTag(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value && !this.selectedTags.includes(value)) {
      this.selectedTags.push(value);
    }
    event.chipInput!.clear();
    this.tagControl.setValue(null);
  }

  removeTag(tag: string): void {
    const index = this.selectedTags.indexOf(tag);
    if (index >= 0) {
      this.selectedTags.splice(index, 1);
    }
  }

  selectTag(event: MatAutocompleteSelectedEvent): void {
    const value = event.option.viewValue;
    if (!this.selectedTags.includes(value)) {
      this.selectedTags.push(value);
    }
    this.tagInput.nativeElement.value = '';
    this.tagControl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.availableTags.filter(tag =>
      tag.toLowerCase().includes(filterValue) &&
      !this.selectedTags.includes(tag)
    );
  }
}
