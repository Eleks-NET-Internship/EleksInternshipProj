import { Component, OnInit } from '@angular/core';
import { EventsService } from '../../services/events.service';
import { CreateEventDto, EventDto } from '../../models/events-models';
import { map } from 'rxjs';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AddEventDialogComponent } from '../add-event-dialog/add-event-dialog.component';
import { SpaceContextService } from '../../../../core/services/space-context/space-context.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {
  events: EventDto[] = [];
  selectedEvent!: EventDto;
  private spaceId: number | null = null;

  constructor(
    private eventsService: EventsService,
    private dialog: MatDialog,
    private router: Router,
    private spaceContextService: SpaceContextService
  ) { }

  ngOnInit(): void {
    this.spaceId = this.spaceContextService.getSpaceId();
    if (!this.spaceId) {
      console.log("No spaceId in events init");
      return;
    }

    this.eventsService.getAll(this.spaceId)
      .pipe(map((response: { data: EventDto[] }) => response.data))
      .subscribe({
        next: (events) => {
          this.events = events;
        },
        error: (err) => {
          console.error('Помилка при завантаженні подій:', err);
        }
      });
  }

  addEvent(): void {
    if (!this.spaceId) {
      console.log("No spaceId in events");
      return;
    }

    const dialogRef = this.dialog.open(AddEventDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (!this.spaceId) {
          console.log("No spaceId in events lambda");
          return;
        }
        const newEvent: CreateEventDto = {
          name: result,
          spaceId: this.spaceId
        };

        this.eventsService.create(newEvent).subscribe({
          next: (response) => {
            const addedEvent: EventDto = {
              id: response.data,
              name: newEvent.name,
              spaceId: newEvent.spaceId,
              markers: []
            };
            this.events.push(addedEvent);
            console.log('Подію збережено з ID:', response.data);
          },
          error: (err) => {
            console.error('Помилка при створенні події:', err);
          }
        });
      }
    });
  }

  editEvent(): void {
    if (!this.selectedEvent) return;

    const dialogRef = this.dialog.open(AddEventDialogComponent, {
      width: '400px',
      data: this.selectedEvent.name
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result && result !== this.selectedEvent.name) {
        const updatedEvent = {
          name: result,
          id: this.selectedEvent.id
        };

        this.eventsService.update(this.selectedEvent.id, updatedEvent).subscribe({
          next: () => {
            this.selectedEvent.name = result;
          },
          error: (err) => {
            console.error('Помилка при оновленні події:', err);
          }
        });
      }
    });
  }

  deleteEvent(): void {
    if (!this.selectedEvent) return;

    this.eventsService.delete(this.selectedEvent.id).subscribe({
      next: () => {
        this.events = this.events.filter(e => e.id !== this.selectedEvent.id);
      },
      error: (err) => {
        console.error('Помилка при видаленні події:', err);
      }
    });
  }

  goToEvent(event: EventDto): void {
    this.router.navigate(['/events', event.id]);
  }
}
