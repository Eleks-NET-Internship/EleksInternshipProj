import { Component, Inject, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { COMMA, ENTER } from '@angular/cdk/keycodes';

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
  
  // Доступні мітки для автозаповнення
  availableTags: string[] = [
    'робота', 'особисте', 'здоров\'я', 'спорт', 'навчання',
    'зустріч', 'проект', 'дедлайн', 'відпочинок', 'подорож',
    'сім\'я', 'друзі', 'покупки', 'лікар', 'важливо'
  ];
  
  selectedTags: string[] = [];
  filteredTags!: Observable<string[]>;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  tagControl = new FormControl('');

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EventEditFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EventData
  ) {
    this.eventForm = this.createForm();
    this.filteredTags = this.tagControl.valueChanges.pipe(
      startWith(''),
      map((value: string | null) => value ? this._filter(value) : this.availableTags.slice())
    );
  }

  ngOnInit(): void {
    if (this.data) {
      this.eventForm.patchValue({
        name: this.data.name || '',
        startTime: this.data.startTime || '',
        endTime: this.data.endTime || '',
        tags: this.data.tags || ''
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

  onSave(): void {
  if (this.eventForm.valid) {
    const formValue = this.eventForm.value;
    this.dialogRef.close({
      ...this.data,
      ...formValue
    });
  } else {
    this.eventForm.markAllAsTouched(); // Важливо!
  }
}


  onCancel(): void {
    this.dialogRef.close();
  }

  addTag(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value && !this.selectedTags.includes(value)) {
      this.selectedTags.push(value);
      this.updateFormTags();
    }
    event.chipInput!.clear();
    this.tagControl.setValue(null);
  }

  removeTag(tag: string): void {
    const index = this.selectedTags.indexOf(tag);
    if (index >= 0) {
      this.selectedTags.splice(index, 1);
      this.updateFormTags();
    }
  }

  selectTag(event: MatAutocompleteSelectedEvent): void {
    const value = event.option.viewValue;
    if (!this.selectedTags.includes(value)) {
      this.selectedTags.push(value);
      this.updateFormTags();
    }
    this.tagInput.nativeElement.value = '';
    this.tagControl.setValue(null);
  }

  private updateFormTags(): void {
    this.eventForm.patchValue({ tags: this.selectedTags });
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.availableTags.filter(tag => 
      tag.toLowerCase().includes(filterValue) && 
      !this.selectedTags.includes(tag)
    );
  }
}