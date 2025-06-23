import { Component, ElementRef, inject, OnInit, signal, ViewChild } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { map, Observable } from 'rxjs';
import { CalendarService } from '../../services/calendar.service';
import { MarkerDto } from '../../models/calendar-models';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.css'
})
export class AddEventComponent implements OnInit {
  selectedTags: string[] = [];
  separatorKeysCodes: number[] = [ENTER, COMMA];
  tagInput!: ElementRef<HTMLInputElement>;
  inputValue = signal('');
  tags!: Observable<MarkerDto[]>;
  filteredTags!: Observable<string[]>;
  eventData: {eventName: string, eventTime: string, markerNames: string[]} = {
    eventName: '',
    eventTime: '',
    markerNames: []
  }

  calendarService = inject(CalendarService);

  @ViewChild('tagInput') set tagInputRef(ref: ElementRef<HTMLInputElement>) {
    if (ref) {
      this.tagInput = ref;
    }
  };

  constructor(public dialogRef: MatDialogRef<AddEventComponent>) {}

  ngOnInit(): void {
    this.tags = this.calendarService.getMarkersBySpace();
    this.filteredTags = this.tags.pipe(
      map(markers => markers.map(marker => marker.name))
    );
  }

  onSave() {
    if (this.eventData.eventName.trim()) {
      this.eventData.markerNames = [...this.selectedTags];
      this.dialogRef.close(this.eventData);
    }
  }

  onCancel() {
    this.dialogRef.close();
  }

  addTag(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value && !this.selectedTags.includes(value)) {
      this.selectedTags.push(value);
    }
    event.chipInput.clear();
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
  }
}
