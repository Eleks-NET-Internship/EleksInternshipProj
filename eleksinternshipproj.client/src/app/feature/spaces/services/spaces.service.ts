import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SpaceDto } from '../models/spaces-models';

@Injectable({
  providedIn: 'root'
})
export class SpacesService {
  private readonly apiBaseUrl = 'https://localhost:7050';

  constructor(private readonly http: HttpClient) { }

  getSpaces(): Observable<SpaceDto[]> {
    return this.http.get<SpaceDto[]>(`${this.apiBaseUrl}/api/Space/all`);
  }

  createSpace(spaceName: string): Observable<SpaceDto> {
    return this.http.post<SpaceDto>(`${this.apiBaseUrl}/api/Space/add`, spaceName);
  }

  deleteSpace(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/api/Space/delete/${id}`);
  }

  renameSpace(space: SpaceDto): Observable<SpaceDto> {
    return this.http.patch<SpaceDto>(`${this.apiBaseUrl}/api/Space/rename/${space.id}`, space.name);
  }
}
