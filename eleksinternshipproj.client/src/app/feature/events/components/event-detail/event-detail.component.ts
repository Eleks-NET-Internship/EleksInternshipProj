import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventsService } from '../../services/events.service';
import { TaskDto } from '../../../tasks/models/tasks-models';
import { TasksService } from '../../../tasks/services/tasks.service';
import { MatDialog } from '@angular/material/dialog';
import { map } from 'rxjs';
import { EventDto } from '../../models/events-models';
import { TaskDetailsDialogComponent } from '../../../tasks/components/task-details-dialog/task-details-dialog.component';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.css'
})
export class EventDetailComponent implements OnInit {
  eventId!: number;
  event?: EventDto;
  tasks: TaskDto[] = [];

  constructor(
    private route: ActivatedRoute,
    private eventsService: EventsService,
    private taskService: TasksService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.eventId = Number(this.route.snapshot.paramMap.get('id'));
    this.eventsService.getById(this.eventId).subscribe({
      next: (response) => {
        this.event = response.data;
        this.loadTasks();
      },
      error: (err) => {
        console.error('Помилка при завантаженні події:', err);
      }
    });
  }

  loadTasks(): void {
    this.taskService.getAllByEventId(this.eventId)
      .pipe(map((response: { data: TaskDto[] }) => response.data))
      .subscribe({
        next: (tasks) => {
          const now = new Date();

          this.tasks = tasks.sort((a, b) => {
            const dateA = new Date(a.eventTime);
            const dateB = new Date(b.eventTime);

            const isPastA = dateA.getTime() < now.getTime();
            const isPastB = dateB.getTime() < now.getTime();

            if (isPastA && !isPastB) return 1;
            if (!isPastA && isPastB) return -1;

            return dateA.getTime() - dateB.getTime();
          });
        },
        error: (err) => {
          console.error('Помилка при завантаженні завдань:', err);
        }
      });
  }

  openTaskDetails(task: TaskDto): void {
    const dialogRef = this.dialog.open(TaskDetailsDialogComponent, {
      width: '700px',
      data: { ...task }
    });

    dialogRef.afterClosed().subscribe((updatedTask: TaskDto) => {
      if (updatedTask) {
        this.taskService.update(updatedTask.id, updatedTask).subscribe({
          next: () => {
            const index = this.tasks.findIndex(t => t.id === updatedTask.id);
            if (index > -1) {
              this.tasks[index] = updatedTask;
            }
          },
          error: (err) => {
            console.error('Не вдалося оновити завдання:', err);
          }
        });
      }
    });
  }


    deleteTask(id: number, event: MouseEvent): void {
    event.stopPropagation();
    if (confirm('Ви впевнені, що хочете видалити це завдання?')) {
      this.taskService.delete(id).subscribe({
        next: () => {
          this.tasks = this.tasks.filter(t => t.id !== id);
        },
        error: err => console.error('Помилка при видаленні завдання:', err)
      });
    }
  }
}
