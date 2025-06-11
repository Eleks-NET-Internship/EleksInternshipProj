import { Component } from '@angular/core';
import { EventsService } from '../../services/events.service';
import { EventDto } from '../../models/events-models';
import { map } from 'rxjs';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrl: './events.component.css'
})
export class EventsComponent {
  constructor(private eventsService: EventsService) { }

    ngOnInit(): void {
     // const spaceId = this.spaceService.getSpaceId();     Тут буде витягнення spaceId з сервісу, коли він буде реалізований, поки що дефолтне значення 1
    const spaceId = 1;  
    this.eventsService.getAll(spaceId)
      .pipe(
        map((response: { data: EventDto[] }) => response.data)
      )
      .subscribe(events => {
        console.log(events); 
      });
  }
}
