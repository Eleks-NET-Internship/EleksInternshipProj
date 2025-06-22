import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TaskDto, TaskModelStatusDto } from '../../models/tasks-models';
import { TasksService } from '../../services/tasks.service';
import { map } from 'rxjs';
import { EventsService } from '../../../events/services/events.service';
import { EventDto } from '../../../events/models/events-models';

const DEFAULT_MIN_DATE = '0001-01-01T00:00:00';

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
    this.getEventById(this.task.eventId);

    if (!this.task.name) {
      this.task.name = 'Очікує';
    }

    if (this.task.eventTime && this.task.eventTime !== DEFAULT_MIN_DATE) {
      const dt = new Date(this.task.eventTime);

      // Локальна дата для input[type="date"]
      const year = dt.getFullYear();
      const month = String(dt.getMonth() + 1).padStart(2, '0');
      const day = String(dt.getDate()).padStart(2, '0');
      this.eventDate = `${year}-${month}-${day}`;

      // Локальний час для input[type="time"]
      const hours = String(dt.getHours()).padStart(2, '0');
      const minutes = String(dt.getMinutes()).padStart(2, '0');
      this.eventTimeString = `${hours}:${minutes}`;
    } else {
      this.eventDate = '';
      this.eventTimeString = '';
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
          console.error('Помилка при завантаженні статусів:', err);
        }
      });
  }

  getEventById(eventId: number) {
    this.eventService.getById(eventId)
      .pipe(map((response: { data: EventDto }) => response.data))
      .subscribe({
        next: (event) => {
          this.event = event;
          this.eventName = event.name;
        },
        error: (err) => {
          console.error('Помилка при завантаженні події:', err);
        }
      });
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    if (this.eventDate?.trim() && this.eventTimeString?.trim()) {
      const [year, month, day] = this.eventDate.split('-').map(Number);
      const [hours, minutes] = this.eventTimeString.split(':').map(Number);

      const localDate = new Date(year, month - 1, day, hours, minutes);
      this.task.eventTime = localDate.toISOString();
    } else {
      this.task.eventTime = DEFAULT_MIN_DATE;
    }

    this.dialogRef.close(this.task);
  }
}
