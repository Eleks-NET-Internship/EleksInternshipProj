import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventsService } from '../../services/events.service';
import { TaskDto } from '../../../tasks/models/tasks-models';
import { TasksService } from '../../../tasks/services/tasks.service';
import { MatDialog } from '@angular/material/dialog';
import { map } from 'rxjs';
import { EventDto } from '../../models/events-models';
import { TaskDetailsDialogComponent } from '../../../tasks/components/task-details-dialog/task-details-dialog.component';
import { NoteDto, NoteService } from '../../../../core/services/note/note.service';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.css'
})
export class EventDetailComponent implements OnInit {
  eventId!: number;
  event?: EventDto;
  tasks: TaskDto[] = [];
  notes: NoteDto[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventsService: EventsService,
    private taskService: TasksService,
    private noteService: NoteService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.eventId = Number(this.route.snapshot.paramMap.get('id'));
    this.eventsService.getById(this.eventId).subscribe({
      next: (response) => {
        this.event = response.data;
        this.loadTasks();
        this.loadNotes();
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

   loadNotes(): void {
    this.noteService.getNotesByEventId(this.eventId)
      .then(notes => {
        this.notes = notes;
      })
      .catch(err => {
        console.error('Помилка при завантаженні нотаток:', err);
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

  openNote(note: NoteDto): void {
     this.router.navigate(['/notes', note.id]);
  }
}
