import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.css'
})
export class AddEventComponent {
  eventName: string = '';
  eventTime: string = '';

  constructor(public dialogRef: MatDialogRef<AddEventComponent>) {}

  onSave() {
    if (this.eventName.trim()) {
      this.dialogRef.close({ eventName: this.eventName, eventTime: this.eventTime });
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
