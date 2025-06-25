import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskDto, TaskModelStatusDto } from '../models/tasks-models';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private readonly apiBaseUrl = 'https://localhost:7050/api/Task';

  constructor(private readonly http: HttpClient) { }
  
  getAllByEventId(eventId: number): Observable<{ data: TaskDto[] }> {
    return this.http.get<{ data: TaskDto[] }>(`${this.apiBaseUrl}/event/${eventId}`);
  }

  getAllBySpaceId(spaceId: number): Observable<{ data: TaskDto[] }> {
    return this.http.get<{ data: TaskDto[] }>(`${this.apiBaseUrl}/space/${spaceId}`);
  }

  getAllByStatus(spaceId: number, statusId: number): Observable<{ data: TaskDto[] }> {
    return this.http.get<{ data: TaskDto[] }>(`${this.apiBaseUrl}/space/${spaceId}/status/${statusId}`);
  }

  getById(id: number): Observable<{ data: TaskDto }> {
    return this.http.get<{ data: TaskDto }>(`${this.apiBaseUrl}/${id}`);
  }

  create(dto: TaskDto): Observable<{ message: string; data: number }> {
    return this.http.post<{ message: string; data: number }>(`${this.apiBaseUrl}`, dto);
  }

  update(id: number, dto: TaskDto): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.apiBaseUrl}/update/${id}`, dto);
  }

  updateStatus(id: number, newStatusId: number): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.apiBaseUrl}/update-status/${id}/${newStatusId}`, {});
  }

  delete(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.apiBaseUrl}/${id}`);
  }

  getAllStatuses(): Observable<{ data: TaskModelStatusDto[] }> {
    return this.http.get<{ data: TaskModelStatusDto[] }>(`${this.apiBaseUrl}/statuses`);
  }
}
