import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.css'
})
export class AddEventComponent {
  eventData = {
    eventName: '',
    eventTime: ''
  }

  constructor(public dialogRef: MatDialogRef<AddEventComponent>) {}

  onSave() {
    if (this.eventData.eventName.trim()) {
      this.dialogRef.close(this.eventData);
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
