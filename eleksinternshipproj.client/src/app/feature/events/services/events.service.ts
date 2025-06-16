import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateEventDto, EventDto, UpdateEventDto } from '../models/events-models';

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  private readonly apiBaseUrl = 'https://localhost:7050/api/RoutineEvents';

  constructor(private readonly http: HttpClient) { }

  getAll(spaceId: number): Observable<{ data: EventDto[] }> {
    return this.http.get<{ data: EventDto[] }>(`${this.apiBaseUrl}/all/${spaceId}`);
  }

  getById(id: number): Observable<{ data: EventDto }> {
    return this.http.get<{ data: EventDto }>(`${this.apiBaseUrl}/${id}`);
  }

  create(dto: CreateEventDto): Observable<{ message: string; data: number }> {
    return this.http.post<{ message: string; data: number }>(`${this.apiBaseUrl}`, dto);
  }

  update(id: number, dto: UpdateEventDto): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.apiBaseUrl}/update/${id}`, dto);
  }

  delete(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.apiBaseUrl}/${id}`);
  }
}
