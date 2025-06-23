import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { NoteService, NoteDto } from '../../../../core/services/note/note.service';

@Component({
  selector: 'app-event-notes',
  templateUrl: './event-notes.component.html',
  styleUrls: ['./event-notes.component.scss']
})
export class EventNotesComponent implements OnInit {
  eventId: number = 1;
  eventTitle: string = 'Подія';

  eventNotes: NoteDto[] = [];
  loading: boolean = false;

  constructor(
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    private noteService: NoteService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(async params => {
      this.eventId = +params['eventId'] || 1;
      this.eventTitle = params['eventTitle'] || 'Подія';
      await this.loadEventNotes();
    });
  }

  async loadEventNotes(): Promise<void> {
    this.loading = true;
    try {
      this.eventNotes = await this.noteService.getNotesByEventId(this.eventId);
      console.log('Завантажено нотаток для події', this.eventId, this.eventNotes);
    } catch (error) {
      console.error('Помилка завантаження нотаток для події:', error);
      this.eventNotes = [];
    } finally {
      this.loading = false;
    }
  }

  addNote(): void {
    this.router.navigate(['/notes', 'new']);
    console.log('Додати нотатку');
  }

  editNote(note: NoteDto): void {
    this.router.navigate(['/notes', note.id]);
    console.log('Редагувати нотатку:', note);
  }

  async deleteNote(noteId: number): Promise<void> {
    if (!confirm('Ви впевнені, що хочете видалити цю нотатку?')) return;

    try {
      await this.noteService.deleteNote(noteId);
      this.eventNotes = this.eventNotes.filter(note => note.id !== noteId);
      console.log('Нотатку видалено:', noteId);
    } catch (error) {
      console.error('Помилка видалення нотатки:', error);
      alert('Не вдалося видалити нотатку');
    }
  }

  goBack(): void {
    this.location.back();
  }
}
