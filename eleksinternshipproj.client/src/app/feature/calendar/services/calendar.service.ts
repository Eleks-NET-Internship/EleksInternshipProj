import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

import type { TaskDTO, UniqueEventDTO } from '../models/calendar-models';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {
  private readonly apiBaseUrl = 'https://localhost:7050';

  constructor(private readonly http: HttpClient) { }
  
  getTasksByDate(date: Date): Observable<TaskDTO[]> {
  return this.http.get<{ data: TaskDTO[] }>(this.apiBaseUrl + '/api/Task/space/1').pipe(
    map(res => res.data.filter(task => {
      const taskDate = new Date(task.eventTime);
      return this.areSameDate(date, taskDate);
    }))
  );
}

  private areSameDate(d1: Date, d2: Date): boolean {
    return d1.getFullYear() === d2.getFullYear() &&
          d1.getMonth() === d2.getMonth() &&
          d1.getDate() === d2.getDate();
  }

  getTasksWithinWeek(date: Date): Observable<TaskDTO[]> {
    date.setHours(0, 0, 0, 0);

    const oneWeekFromToday = new Date(date);
    oneWeekFromToday.setDate(date.getDate() + 7);

    return this.http.get<{ data: TaskDTO[] }>(this.apiBaseUrl + '/api/Task/space/1').pipe(
      map(res => res.data.filter(task => {
        const d = new Date(task.eventTime);
        d.setHours(0, 0, 0, 0);

        return d >= date && d <= oneWeekFromToday;
      }))
    );
  }

  getEventsByDate(date: Date): Observable<UniqueEventDTO[]> {
    return this.http.get<{ data: UniqueEventDTO[] }>(this.apiBaseUrl + '/api/UniqueEvents/all/1').pipe(
      map(res => res.data.filter(event => {
        const eventDate = new Date(event.eventTime);
        return this.areSameDate(date, eventDate);
      }))
    );
  }
}
