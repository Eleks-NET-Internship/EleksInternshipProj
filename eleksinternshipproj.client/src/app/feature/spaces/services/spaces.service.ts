import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../shared/.env/environment';
import { SpaceDto, SpaceDtoShort, SpaceRenameDto, UserSpaceDto } from '../models/spaces-models';

@Injectable({
  providedIn: 'root'
})
export class SpacesService {
  private readonly apiBaseUrl = `${environment.apiUrl}/api/Space`;

  constructor(private readonly http: HttpClient) { }

  getSpaces(): Observable<SpaceDto[]> {
    return this.http.get<SpaceDto[]>(`${this.apiBaseUrl}/all`);
  }

  getSpacesWhereAdmin(): Observable<SpaceRenameDto[]> {
    return this.http.get<SpaceRenameDto[]>(`${this.apiBaseUrl}/where-admin`);
  }

  createSpace(spaceName: string): Observable<SpaceDto> {
    if (!spaceName || spaceName.trim() === '') {
      throw new Error('Space name cannot be empty');
    }

    const spaceDtoShort: SpaceDtoShort = {
      id: 0,
      name: spaceName.trim(),
      userSpaces: [
        {
          id: 0,
          userId: 0,
          spaceId: 0,
          roleId: 1
        }
      ],
      timetable: {
        id: 0,
        spaceId: 0
      }
    }

    return this.http.post<SpaceDto>(this.apiBaseUrl, spaceDtoShort, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  addUserToSpace(spaceId: number, username: string): Observable<UserSpaceDto> {
    return this.http.post<UserSpaceDto>(`${this.apiBaseUrl}/${spaceId}/add/${encodeURIComponent(username)}`, {});
  }



  deleteSpace(spaceId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/${spaceId}`);
  }

  renameSpace(spaceId: number, newName: string): Observable<SpaceDto> {
    const spaceRenameDto: SpaceRenameDto =
    {
      id: spaceId,
      name: newName.trim()
    };
    return this.http.patch<SpaceDto>(
      `${this.apiBaseUrl}`,
      spaceRenameDto,
      { headers: { 'Content-Type': 'application/json' } }
    );
  }
}
