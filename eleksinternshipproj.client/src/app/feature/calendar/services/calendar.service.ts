import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { map, Observable, shareReplay } from 'rxjs';
import { environment } from '../../../shared/.env/environment';

import type { AddUniqueEventDto, TaskDTO, UniqueEventDTO } from '../models/calendar-models';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {
  private readonly apiBaseUrl = environment.apiUrl;
  private spaceId = JSON.parse(sessionStorage.getItem('selectedSpace') ?? '').id;
  private tasks!: Observable<{ data: TaskDTO[] }>;

  trigger = signal<boolean>(false);

  constructor(private readonly http: HttpClient) { }

  getTasks() {
    this.spaceId = JSON.parse(sessionStorage.getItem('selectedSpace') ?? '').id;
    this.tasks = this.http.get<{ data: TaskDTO[] }>(this.apiBaseUrl + '/api/Task/space/' + this.spaceId).pipe(shareReplay(1));
  }
  
  getTasksByDate(date: Date): Observable<TaskDTO[]> {
  return this.tasks.pipe(
    map(res => res.data.filter(task => {
      const taskDate = new Date(task.eventTime);
      return this.areSameDate(date, taskDate);
    }))
  );
}

  //returns tasks within week except today and tomorrow tasks
  getTasksWithinWeek(): Observable<TaskDTO[]> {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const oneWeekFromToday = this.addDays(today, 6);
    const tomorrow = this.addDays(today, 2);

    return this.tasks.pipe(
      map(res => res.data.filter(task => {
        const d = new Date(task.eventTime);
        d.setHours(0, 0, 0, 0);

        return d >= tomorrow && d <= oneWeekFromToday;
      }))
    );
  }

  getEventsByDate(date: Date): Observable<UniqueEventDTO[]> {
    return this.http.get<{ data: UniqueEventDTO[] }>(this.apiBaseUrl + '/api/UniqueEvents/all/' + this.spaceId).pipe(
      map(res => res.data.filter(event => {
        const eventDate = new Date(event.eventTime);
        return this.areSameDate(date, eventDate);
      }))
    );
  }

  addUniqueEvent(event: AddUniqueEventDto) {
    event.spaceId = Number(this.spaceId);
    this.http.post<{ message: string, data: { id: number, eventId: number } }>(this.apiBaseUrl + '/api/UniqueEvents', event).subscribe({
      next: (response) => {
        console.log('Unique event successfully created with ID=' + response.data.id);
        this.trigger.set(true);
      },
      error: (error) => {
        console.log('Error creating unique event: ', error);
      }
    });
  }

  private areSameDate(d1: Date, d2: Date): boolean {
    return d1.getFullYear() === d2.getFullYear() &&
          d1.getMonth() === d2.getMonth() &&
          d1.getDate() === d2.getDate();
  }

  private addDays(date: Date, days: number): Date {
    const result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }
}
