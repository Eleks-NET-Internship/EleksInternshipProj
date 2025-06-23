import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export interface NoteDto {
  id: number;
  title: string;
  content: string;
  eventId: number;
}

export interface EventDto {
  eventId: number;
  eventName: string;
}

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private readonly apiUrl = 'https://localhost:7050/api/note'; // додав слеш на початку

  constructor(private http: HttpClient) {}

  // Отримати всі нотатки
  async getAllNotes(spaceId: number): Promise<NoteDto[]> {
    try {
      console.log('Запит до API:', this.apiUrl);
      const result = await firstValueFrom(this.http.get<NoteDto[]>(`${this.apiUrl}/space/${spaceId}`));
      console.log('Отримано відповідь від API:', result);
      return result;
    } catch (error) {
      console.error('Помилка API запиту:', error);
      throw error;
    }
  }

  // Отримати нотатку за ID
  async getNoteById(id: number): Promise<NoteDto | null> {
    try {
      return await firstValueFrom(this.http.get<NoteDto>(`${this.apiUrl}/${id}`));
    } catch (error) {
      return null;
    }
  }

  // Отримати нотатки за ID події
  async getNotesByEventId(eventId: number): Promise<NoteDto[]> {
    return firstValueFrom(this.http.get<NoteDto[]>(`${this.apiUrl}/by-event/${eventId}`));
  }

  // Створити нову нотатку
  async createNote(noteDto: NoteDto): Promise<void> {
    await firstValueFrom(this.http.post<void>(this.apiUrl, noteDto));
  }

  // Оновити нотатку
  async updateNote(id: number, noteDto: NoteDto): Promise<void> {
    await firstValueFrom(this.http.put<void>(`${this.apiUrl}/${id}`, noteDto));
  }

  // Видалити нотатку
  async deleteNote(id: number): Promise<void> {
    await firstValueFrom(this.http.delete<void>(`${this.apiUrl}/${id}`));
  }

 async getEvents(spaceId: number): Promise<any[]> {
  const res = await firstValueFrom(
    this.http.get<any>(`https://localhost:7050/api/routineevents/all/${spaceId}`)
  );
  
  return res.data ?? [];
}
}





