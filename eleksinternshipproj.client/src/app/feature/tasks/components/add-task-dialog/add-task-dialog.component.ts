import { Component, OnInit } from '@angular/core';
import { EventDto } from '../../../events/models/events-models';
import { MatDialogRef } from '@angular/material/dialog';
import { TaskDto } from '../../models/tasks-models';
import { EventsService } from '../../../events/services/events.service';
import { map } from 'rxjs';
import { SpaceContextService } from '../../../../core/services/space-context/space-context.service';

@Component({
  selector: 'app-add-task-dialog',
  templateUrl: './add-task-dialog.component.html',
  styleUrl: './add-task-dialog.component.css'
})
export class AddTaskDialogComponent implements OnInit {
  task: Partial<TaskDto> = { name: '', eventId: 0 };
  events: EventDto[] = [];
  private spaceId: number | null = null;

  constructor(
    private dialogRef: MatDialogRef<AddTaskDialogComponent>,
    private eventsService: EventsService,
    private spaceContextService: SpaceContextService
  ) {
    
  }

  ngOnInit(): void {
    this.spaceId = this.spaceContextService.getSpaceId();
    if (!this.spaceId) {
      console.log("No spaceId in addTaskDialog");
      return;
    }

    this.eventsService.getAll(this.spaceId)
      .pipe(map((res: { data: EventDto[] }) => res.data))
      .subscribe(events => this.events = events);
  }

  save(): void {
    if (this.task.name && this.task.eventId) {
      this.dialogRef.close(this.task);
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
