import { Component, OnInit } from '@angular/core';
import { TimetableDto } from '../../models/schedule-models'
import { Router } from '@angular/router';
import {ScheduleService} from '../../services/schedule.service';
import {TimetableContextService} from '../../services/timetable-context-service';
import {SpaceContextService} from '../../../../core/services/space-context/space-context.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {
  daysOfWeek = ['Понеділок', 'Вівторок', 'Середа', 'Четвер', 'П\'ятниця'];

  timetable: TimetableDto | null = null;
  constructor(
    private scheduleService: ScheduleService,
    private context: TimetableContextService,
    private spaceContextService: SpaceContextService,
    private router: Router) {}

  scheduleData: { [key: string]: any[] } = {};

  private spaceId: number | null = null;

  ngOnInit(): void {
    this.spaceId = this.spaceContextService.getSpaceId();
    if (!this.spaceId) {
      console.log("No spaceId in events init");
      return;
    }
    this.scheduleService.getBySpace(this.spaceId).subscribe({
      next: (data) => {
        this.timetable = data;
        this.prepareScheduleData();
      },
      error: (err) => {
        console.error('Failed to load timetable', err);
      }
    });
  }

  prepareScheduleData(): void {
    if (!this.timetable) return;

    this.scheduleData = {};

    for (const day of this.timetable.days) {
      const cards = day.eventDays.map(ed => ({
        time: `${ed.startTime.substring(0, 5)} - ${ed.endTime.substring(0, 5)}`,
        title: ed.event.name,
        description: ed.event.markers.map(m => m.marker.name).join(', '),
        hasNotification: false // You can set a real condition here
      }));

      this.scheduleData[day.dayName] = cards;
    }
  }

  onCardClick(card: any) {
    console.log('Card clicked:', card);
  }

  EditSchedule() {
    this.context.currentTimetable = this.timetable;
    this.router.navigate(['schedule/edit-schedule']);
  }
}
