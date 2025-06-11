import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  private readonly apiBaseUrl = 'https://localhost:7050';

  constructor(private readonly http: HttpClient) { }
  // switch any for model type
  updateStuff(newData: any) {
    return this.http.patch(`${this.apiBaseUrl}/api/...`, newData);
  }

  getStuff(): Observable<any> {
    return this.http.get(`${this.apiBaseUrl}/api/...`);
  }
}
