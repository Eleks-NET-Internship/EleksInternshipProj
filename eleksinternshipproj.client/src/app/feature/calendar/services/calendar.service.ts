import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { firstValueFrom, map, Observable, shareReplay } from 'rxjs';
import { environment } from '../../../shared/.env/environment';

import type { AddUniqueEventDto, MarkerDto, TaskDTO, UniqueEventDTO } from '../models/calendar-models';
import { SpaceContextService } from '../../../core/services/space-context/space-context.service';
import { ScheduleService } from '../../schedule/services/schedule.service';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {
  private readonly apiBaseUrl = environment.apiUrl;
  private spaceId: number | null = null;
  private tasks!: Observable<{ data: TaskDTO[] }>;
  private markers!: MarkerDto[];

  trigger = signal<boolean>(false);

  constructor(private readonly http: HttpClient, private readonly spaceContextService: SpaceContextService, private readonly scheduleService: ScheduleService) { }

  initContext() {
    this.spaceId = this.spaceContextService.getSpaceId();
  }

  getTasks() {
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

  addUniqueEvent(event: AddUniqueEventDto, markers: string[]) {
    event.spaceId = Number(this.spaceId);
    this.http.post<{ message: string, data: { id: number, eventId: number } }>(this.apiBaseUrl + '/api/UniqueEvents', event).subscribe({
      next: (response) => {
        console.log('Unique event successfully created with ID=' + response.data.id);
        this.setMarkersForEvent(response.data.eventId, markers);
        this.trigger.set(true);
      },
      error: (error) => {
        console.log('Error creating unique event: ', error);
      }
    });
  }

  deleteUniqueEvent(eventId: number) {
    this.http.delete(this.apiBaseUrl + '/api/UniqueEvents/' + eventId).subscribe({
      next: () => this.trigger.set(true)
    });
  }

  getMarkersBySpace() {
    if (this.spaceId) {
      const markers = this.scheduleService.getMarkersBySpace(this.spaceId);
      markers.subscribe({
        next: markers => this.markers = markers
      });
      return markers;
    }

    throw Error();
  }

  setMarkersForEvent(eventId: number, markers: string[]) {
    markers.forEach(async marker => {
      if (this.markers.map(value => value.name).includes(marker)){
        this.setExistingMarker(eventId, this.markers.find(value => value.name === marker)?.id ?? 0);
      }
      else {
        this.setExistingMarker(eventId, await this.createMarker(marker));
      }
    });
  }

  private setExistingMarker(eventId: number, markerId: number) {
    this.http.post(`${this.apiBaseUrl}/api/Marker/add-to-event?eventId=${eventId}&markerId=${markerId}`, null).subscribe();
  }

  private async createMarker(name: string) {
    let markerId;
    const markerDto = {
      name: name,
      type: "string",
      spaceId: this.spaceId
    };
    const response: any = await firstValueFrom(this.http.post(`${this.apiBaseUrl}/api/Marker`, markerDto));
    return response.data.id;
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
