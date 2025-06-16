import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NoteService, NoteDto, EventDto } from '../../../../core/services/note/note.service';

@Component({
  selector: 'app-note-detail',
  templateUrl: './note-detail.component.html',
  styleUrls: ['./note-detail.component.scss']
})
export class NoteDetailComponent implements OnInit {
  note: NoteDto | null = null;
  editedNote: NoteDto = { id: 0, title: '', content: '', eventId: 0 };
  isEditing = false;
  isNewNote = false;
  noteId: number = 0;
  events: EventDto[] = [];
  private spaceId: number = 1; 


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private noteService: NoteService
  ) {}

  async ngOnInit(): Promise<void> {
    try {
      const rawEvents = await this.noteService.getEvents(this.spaceId);
      this.events = rawEvents.map(event => ({
        eventId: event.id,
        eventName: event.name
      })); // TODO: spaceId може передаватись динамічно

      const idParam = this.route.snapshot.paramMap.get('id');
      if (idParam === 'new') {
        this.createNewNote();
      } else if (idParam !== null) {
        this.noteId = +idParam;
        await this.loadNote();
      } else {
        this.snackBar.open('Некоректний маршрут', 'Закрити', { duration: 3000 });
        this.goBack();
      }
    } catch (error) {
      console.error('Помилка завантаження:', error);
      this.snackBar.open('Не вдалося завантажити дані', 'Закрити', { duration: 3000 });
    }
  }

  async loadNote(): Promise<void> {
    const note = await this.noteService.getNoteById(this.noteId);
    if (!note) {
      this.snackBar.open('Нотатку не знайдено', 'Закрити', { duration: 3000 });
      this.goBack();
      return;
    }
    this.note = note;
    this.editedNote = { ...note };
  }

  createNewNote(): void {
    this.isNewNote = true;
    this.isEditing = true;
    const newId = Date.now(); // тимчасовий ID, не використовується в API
    this.note = { id: newId, title: '', content: '', eventId: 0 };
    this.editedNote = { ...this.note };
  }

  async toggleEdit(): Promise<void> {
    if (this.isEditing) {
      await this.saveNote();
    } else {
      this.isEditing = true;
      this.editedNote = { ...this.note! };
    }
  }

  async saveNote(): Promise<void> {
    if (!this.editedNote.title.trim() && !this.editedNote.content.trim()) {
      this.snackBar.open('Нотатка не може бути порожньою', 'Закрити', { duration: 3000 });
      return;
    }

    try {
      if (this.isNewNote) {
        await this.noteService.createNote(this.editedNote);
        this.snackBar.open('Нотатку створено', 'Закрити', { duration: 2000 });
      } else {
        await this.noteService.updateNote(this.noteId, this.editedNote);
        this.snackBar.open('Нотатку збережено', 'Закрити', { duration: 2000 });
      }
      this.note = { ...this.editedNote };
      this.isEditing = false;
      this.isNewNote = false;
    } catch (error) {
      console.error('Помилка збереження:', error);
      this.snackBar.open('Не вдалося зберегти нотатку', 'Закрити', { duration: 3000 });
    }
  }

  cancelEdit(): void {
    if (this.isNewNote) {
      this.goBack();
    } else {
      this.editedNote = { ...this.note! };
      this.isEditing = false;
    }
  }

  async deleteNote(): Promise<void> {
    const confirmDelete = confirm('Ви впевнені, що хочете видалити цю нотатку?');
    if (!confirmDelete) {
      return;
    }
    
    try {
      await this.noteService.deleteNote(this.noteId);
      this.snackBar.open('Нотатку видалено', 'Закрити', { duration: 2000 });
      this.goBack();
    } catch (error) {
      this.snackBar.open('Не вдалося видалити нотатку', 'Закрити', { duration: 3000 });
    }
  }

  shareNote(): void {
    if (navigator.share) {
      navigator.share({
        title: this.note?.title || 'Нотатка',
        text: this.note?.content || ''
      });
    } else {
      navigator.clipboard.writeText(
        `${this.note?.title}\n\n${this.note?.content}`
      ).then(() => {
        this.snackBar.open('Нотатку скопійовано в буфер обміну', 'Закрити', { duration: 2000 });
      });
    }
  }

  getEventTitle(eventId: number): string {
  const event = this.events.find(e => e.eventId === eventId);
  console.log('Шукаємо eventId:', eventId);
  console.log('Доступні події:', this.events);
  console.log('Знайдена подія:', event);
  return event ? event.eventName : `Подія ${eventId}`;
}


  async duplicateNote(): Promise<void> {
    if (!this.note) return;

    const newNote: NoteDto = {
      id: 0, // ID буде згенеровано на бекенді
      title: `${this.note.title} (копія)`,
      content: this.note.content,
      eventId: this.note.eventId
    };

    try {
      await this.noteService.createNote(newNote);
      this.snackBar.open('Нотатку дубльовано', 'Закрити', { duration: 2000 });
    } catch (error) {
      this.snackBar.open('Не вдалося дублювати нотатку', 'Закрити', { duration: 3000 });
    }
  }

  exportNote(): void {
    const content = `${this.note?.title}\n\n${this.note?.content}`;
    const blob = new Blob([content], { type: 'text/plain' });
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = `${this.note?.title || 'note'}.txt`;
    link.click();
    window.URL.revokeObjectURL(url);
  }

  getFormattedContent(): string {
    if (!this.note?.content) return '';
    return this.note.content
      .replace(/\n/g, '<br>')
      .replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>')
      .replace(/\*(.*?)\*/g, '<em>$1</em>');
  }

  compareEvents(e1: number, e2: number): boolean {
    return e1 === e2;
  }

  clearEvent(): void {
    this.editedNote.eventId = 0;
  }

  goBack(): void {
    this.location.back();
  }
}
