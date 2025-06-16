import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventsService } from '../../services/events.service';
import { EventDto } from '../../models/events-models';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.css'
})
export class EventDetailComponent implements OnInit {
  eventId!: number;
  event?: EventDto;

  constructor(
    private route: ActivatedRoute,
    private eventsService: EventsService
  ) {}

   ngOnInit(): void {
    this.eventId = Number(this.route.snapshot.paramMap.get('id'));

    this.eventsService.getById(this.eventId).subscribe({
      next: (response) => {
        this.event = response.data;
      },
      error: (err) => {
        console.error('Помилка при завантаженні події:', err);
      }
    });
  }
}
