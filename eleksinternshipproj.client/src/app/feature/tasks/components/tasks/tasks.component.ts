import { Component, OnInit } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { TaskDto } from '../../models/tasks-models';
import { map } from 'rxjs';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css'
})
export class TasksComponent implements OnInit {

  constructor(private tasksService: TasksService) { }
   private spaceId: number = 1; 
   tasks: TaskDto[] = [];


  ngOnInit(): void {
     this.tasksService.getAllBySpaceId(this.spaceId)
          .pipe(map((response: { data: TaskDto[] }) => response.data))
          .subscribe({
            next: (tasks) => {
              this.tasks = tasks;
              console.log(tasks);
            },
            error: (err) => {
              console.error('Помилка при завантаженні завданнь:', err);
            }
          });
  }

}
