import { Component, OnInit } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { TaskDto } from '../../models/tasks-models';
import { map } from 'rxjs';
import { TaskDetailsDialogComponent } from '../task-details-dialog/task-details-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AddTaskDialogComponent } from '../add-task-dialog/add-task-dialog.component';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css'
})
export class TasksComponent implements OnInit {

  private spaceId: number;
  tasks: TaskDto[] = [];

  constructor(
    private tasksService: TasksService,
    private dialog: MatDialog
  ) {
    const storedSpace = sessionStorage.getItem('selectedSpace');
    this.spaceId = storedSpace ? JSON.parse(storedSpace).id : 1; 
  }
  
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
            
            return dateA.getTime() - dateB.getTime();
          });
        },
        error: (err) => {
          console.error('Помилка при завантаженні завдань:', err);
        }
      });
  }

  deleteTask(id: number, event: MouseEvent): void {
  event.stopPropagation();
  if (confirm('Ви впевнені, що хочете видалити це завдання?')) {
    this.tasksService.delete(id).subscribe({
      next: () => {
        this.tasks = this.tasks.filter(t => t.id !== id);
      },
      error: err => console.error('Помилка при видаленні завдання:', err)
    });
  }
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
  

  openAddTaskDialog(): void {
  const dialogRef = this.dialog.open(AddTaskDialogComponent, {
    width: '400px'
  });

  dialogRef.afterClosed().subscribe((newTask: Partial<TaskDto>) => {
    if (newTask) {
       const newTaskDto: TaskDto = {
        id: 0,
        name: newTask.name || '',
        description: '',
        eventId: newTask.eventId || 0,
        eventTime: '0001-01-01T00:00:00',
        isDeadline: true,
        statusId: 1, 
        statusName: 'Очікує' 
      };
      this.tasksService.create(newTaskDto).subscribe({
        next: () => this.loadTasks(),
        error: err => console.error('Помилка при створенні завдання:', err)
      });
    }
  });
}

}
