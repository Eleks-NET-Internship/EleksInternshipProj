import { Component, input, inject } from '@angular/core';
import { UniqueEventDTO } from '../../models/calendar-models';
import { CalendarService } from '../../services/calendar.service';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrl: './event.component.css',
})
export class EventComponent {
  calendarService = inject(CalendarService);
  event = input.required<UniqueEventDTO>();

  get time() {
    const date = new Date(this.event().eventTime);

    const hours = date.getHours();
    const minutes = date.getMinutes();

    let hoursString: string = String(hours);
    let minutesString: string = String(minutes);
    
    if (hours < 10) {
      hoursString = "0" + hoursString;
    }

    if (minutes < 10) {
      minutesString = "0" + minutesString;
    }

    return  `${hoursString}:${minutesString}`;
  }

  get markers() {
    const markers = this.event().markers.map(value => value.name);
    return markers.join(',');
  }

  onDeleteEvent() {
    this.calendarService.deleteUniqueEvent(this.event().id);
  }
}
