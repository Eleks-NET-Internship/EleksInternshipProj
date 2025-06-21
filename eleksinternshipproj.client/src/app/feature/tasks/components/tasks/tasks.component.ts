import { Component, OnInit } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { TaskDto } from '../../models/tasks-models';
import { map } from 'rxjs';
import { TaskDetailsDialogComponent } from '../task-details-dialog/task-details-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css'
})
export class TasksComponent implements OnInit {

  constructor(private tasksService: TasksService, private dialog: MatDialog) { }
   private spaceId: number = 1; 
   tasks: TaskDto[] = [];


  ngOnInit(): void {
  this.loadTasks();
}

loadTasks(): void {
  this.tasksService.getAllBySpaceId(this.spaceId)
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
          
          // Обидва або актуальні, або прострочені — за датою
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
      this.tasksService.update(updatedTask.id, updatedTask).subscribe({
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
  
}
