import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {MarkerDto, TimetableDto} from '../models/schedule-models';
import { environment } from '../../../shared/.env/environment';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  private readonly apiBaseUrl = `${environment.apiUrl}/api/TimeTable`;
  private readonly markerBaseUrl = `${environment.apiUrl}/api/Marker`;

  constructor(private readonly http: HttpClient) {}

  /**
   * Get timetable by space ID
   * @param spaceId ID of the space
   */
  getBySpace(spaceId: number): Observable<TimetableDto> {
    return this.http.get<TimetableDto>(`${this.apiBaseUrl}/space/${spaceId}`);
  }

  /**
   * Update the timetable
   * @param timetable Timetable data to update
   */
  updateTimetable(timetable: TimetableDto): Observable<TimetableDto> {
    return this.http.put<TimetableDto>(this.apiBaseUrl, timetable);
  }

  /**
   * Get tags from space
   * @param spaceId ID of the space
   */
  getMarkersBySpace(spaceId: number): Observable<MarkerDto[]> {
    return this.http.get<MarkerDto[]>(`${this.markerBaseUrl}/by-space/${spaceId}`);
  }
}
