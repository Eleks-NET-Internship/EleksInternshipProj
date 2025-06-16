import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NoteService, NoteDto, EventDto } from '../../../../core/services/note/note.service';
import { Router } from '@angular/router';

// Для групування нотаток за подіями
export interface EventGroup {
  eventId: number;
  eventName: string;
  notes: NoteDto[];
}

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.scss']
})
export class NotesComponent implements OnInit {
  regularNotes: NoteDto[] = [];
  eventNotes: EventGroup[] = [];
  events: EventDto[] = [];
  loading = false;
  private spaceId: number = 1; 

  constructor(
    private noteService: NoteService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadEvents();
  }

  private async loadEvents(): Promise<void> {
  try {
    this.loading = true;

    const rawEvents = await this.noteService.getEvents(this.spaceId);
this.events = rawEvents.map(event => ({
  eventId: event.id,
  eventName: event.name
}));
    console.log('Отримані події (сирі):', rawEvents);

    // Мапимо до твого EventDto
    this.events = rawEvents.map(event => ({
      eventId: event.id,
      eventName: event.name
    }));

    console.log('Події після трансформації:', this.events);

    await this.loadNotes();
  } catch (error) {
    console.error('Помилка завантаження подій:', error);
    this.showError('Не вдалося завантажити події');
    await this.loadNotes();
  } finally {
    this.loading = false;
  }
}


  async loadNotes(): Promise<void> {
    try {
      this.loading = true;
      this.regularNotes = await this.noteService.getAllNotes(this.spaceId);
      console.log('Завантажено нотаток:', this.regularNotes.length);
      this.groupNotesByEvents();
      console.log('Групи подій:', this.eventNotes.length);
    } catch (error) {
      console.error('Помилка завантаження нотаток:', error);
      this.showError('Не вдалося завантажити нотатки');
    } finally {
      this.loading = false;
    }
  }

  private groupNotesByEvents(): void {
  this.eventNotes = [];

  const groupedNotes = this.regularNotes.reduce((groups, note) => {
  const eventId = note.eventId; // БЕЗ toString()
  if (!groups[eventId]) {
    groups[eventId] = [];
  }
  groups[eventId].push(note);
  return groups;
}, {} as { [key: number]: NoteDto[] });

Object.keys(groupedNotes).forEach(eventIdStr => {
  const eventId = +eventIdStr; // або parseInt(eventIdStr, 10)
  const eventName = this.getEventName(eventId);

  this.eventNotes.push({
    eventId,
    eventName,
    notes: groupedNotes[eventId]
  });
});


  this.eventNotes.sort((a, b) => a.eventName.localeCompare(b.eventName));
}


private getEventName(eventId: number): string {
  const event = this.events.find(e => e.eventId === eventId);
  console.log('Шукаємо eventId:', eventId);
  console.log('Доступні події:', this.events);
  console.log('Знайдена подія:', event);
  return event ? event.eventName : `Подія ${eventId}`;
}





  addNote(): void {
    this.router.navigate(['/notes', 'new']);
    console.log('Додати нотатку');
  }

  editNote(note: NoteDto): void {
    this.router.navigate(['/notes', note.id]);
    console.log('Редагувати нотатку:', note);
  }

  openEventNotes(event: EventGroup): void {
    this.router.navigate(['/event-notes'], { 
      queryParams: { 
        eventId: event.eventId,
        eventTitle: event.eventName 
      }
    });
  }

  async deleteNote(id: number): Promise<void> {
    const confirmDelete = confirm('Ви впевнені, що хочете видалити цю нотатку?');
    if (!confirmDelete) {
      return;
    }

    try {
      await this.noteService.deleteNote(id);
      this.showSuccess('Нотатку видалено');
      this.loadNotes();
    } catch (error) {
      console.error('Помилка видалення нотатки:', error);
      this.showError('Не вдалося видалити нотатку');
    }
  }

  private showSuccess(message: string): void {
    this.snackBar.open(message, 'Закрити', {
      duration: 3000,
      panelClass: ['success-snackbar']
    });
  }

  private showError(message: string): void {
    this.snackBar.open(message, 'Закрити', {
      duration: 5000,
      panelClass: ['error-snackbar']
    });
  }
}
