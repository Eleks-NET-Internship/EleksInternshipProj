import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProfileDto, ProfileResponse, UpdateProfileDto } from '../models/profile-models';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private readonly apiBaseUrl = 'https://localhost:7050';

  constructor(private readonly http: HttpClient) { }

  updateProfile(newData: UpdateProfileDto) {
    return this.http.patch<UpdateProfileDto>(`${this.apiBaseUrl}/api/profile`, newData);
  }

  getProfile(): Observable<ProfileResponse> {
    return this.http.get<ProfileResponse>(`${this.apiBaseUrl}/api/profile`);
  }
}
