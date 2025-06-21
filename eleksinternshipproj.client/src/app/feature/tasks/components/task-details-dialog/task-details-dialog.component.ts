import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TaskDto, TaskModelStatusDto } from '../../models/tasks-models';
import { TasksService } from '../../services/tasks.service';
import { map } from 'rxjs';
import { EventsService } from '../../../events/services/events.service';
import { EventDto } from '../../../events/models/events-models';

@Component({
  selector: 'app-task-details-dialog',
  templateUrl: './task-details-dialog.component.html',
  styleUrl: './task-details-dialog.component.css'
})
export class TaskDetailsDialogComponent implements OnInit {
  statuses: TaskModelStatusDto[] = [];
  event: EventDto = {} as EventDto;

  eventDate!: string;
  eventTimeString!: string;
  eventName: string = '';

  constructor(
    private tasksService: TasksService,
    private eventService: EventsService,
    public dialogRef: MatDialogRef<TaskDetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public task: TaskDto
  ) {}

  ngOnInit() {
    this.getStatuses();
    this.getEventById(this.task.eventId)
    this.eventName = this.event?.name;

    if (!this.task.name) {
      this.task.name = 'Очікує';
    }

    if (this.task.eventTime) {
      const dt = new Date(this.task.eventTime);

      this.eventDate = dt.toISOString().split('T')[0];

      // Формат часу HH:mm для input[type="time"]
      this.eventTimeString = dt.toTimeString().slice(0, 5);
    } else {
      this.eventDate = '';
      this.eventTimeString = '00:00';
    }
  }

  onStatusChange(selectedStatusId: number) {
  const selectedStatus = this.statuses.find(s => s.id === +selectedStatusId);
  if (selectedStatus) {
    this.task.statusName = selectedStatus.name;
  }
}



  getStatuses() {
     this.tasksService.getAllStatuses()
              .pipe(map((response: { data: TaskModelStatusDto[] }) => response.data))
              .subscribe({
                next: (statuses) => {
                  this.statuses = statuses;
                },
                error: (err) => {
                  console.error('Помилка при завантаженні завданнь:', err);
                }
              });
  }

  getEventById(eventId: number) {
    this.eventService.getById(eventId)
              .pipe(map((response: { data: EventDto }) => response.data))
              .subscribe({
                next: (event) => {
                  this.event = event;
                },
                error: (err) => {
                  console.error('Помилка при завантаженні завданнь:', err);
                }
              });
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    if (this.eventDate && this.eventTimeString) {
      const [year, month, day] = this.eventDate.split('-').map(Number);
      const [hours, minutes] = this.eventTimeString.split(':').map(Number);

      const date = new Date(year, month - 1, day, hours, minutes);
      this.task.eventTime = date.toISOString();
    }

    this.dialogRef.close(this.task);
  }
}
