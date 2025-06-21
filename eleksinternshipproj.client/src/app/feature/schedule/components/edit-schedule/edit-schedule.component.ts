import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Router } from '@angular/router';
import { EventEditFormComponent, EventData } from '../event-edit-form/event-edit-form.component';
import { TimetableContextService } from '../../services/timetable-context-service';
import { ScheduleService } from '../../services/schedule.service';
import { TimetableDto, TimetableDayDto, EventDayDto } from '../../models/schedule-models';

@Component({
  selector: 'app-edit-schedule',
  templateUrl: './edit-schedule.component.html',
  styleUrls: ['./edit-schedule.component.scss']
})
export class EditScheduleComponent implements OnInit {
  timetable: TimetableDto | null = null;
  connectedDropLists: string[] = [];

  readonly allWeekDays = [
    'Понеділок',
    'Вівторок',
    'Середа',
    'Четвер',
    'П’ятниця',
    'Субота',
    'Неділя'
  ];

  constructor(
    private dialog: MatDialog,
    private context: TimetableContextService,
    private scheduleService: ScheduleService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.timetable = structuredClone(this.context.currentTimetable); // Deep clone so edits do not affect original immediately
    if (this.timetable) {
      this.connectedDropLists = this.timetable.days.map((_, i) => `day-${i}`);
    }
  }

  // Returns array of 7 days (TimetableDayDto or null for missing days)
  get daysForDisplay(): (TimetableDayDto | null)[] {
    if (!this.timetable) return [];

    const dayMap = new Map(this.timetable.days.map(d => [d.dayName, d]));
    return this.allWeekDays.map(dayName => dayMap.get(dayName) ?? null);
  }

  onEventDrop(event: CdkDragDrop<EventDayDto[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  addEvent(dayIndex: number) {
    if (!this.timetable) return;

    let day = this.timetable.days.find(d => d.dayName === this.allWeekDays[dayIndex]);

    // If the day is missing, create it and add to timetable
    if (!day) {
      day = {
        id: 0,  // Use 0 or generate a temporary unique id if needed
        dayName: this.allWeekDays[dayIndex],
        timetableId: this.timetable.id,
        eventDays: []
      };
      this.timetable.days.push(day);

      // Rebuild connectedDropLists because days array changed
      this.connectedDropLists = this.timetable.days.map((_, i) => `day-${i}`);
    }

    const newEvent: EventDayDto = {
      id: 0,
      eventId: 0,
      dayId: day.id,
      startTime: '00:00:00',
      endTime: '00:00:00',
      event: {
        id: 0,
        name: 'Нова подія',
        spaceId: this.timetable.spaceId,
        markers: []
      }
    };

    day.eventDays.push(newEvent);
  }

  editEvent(eventDay: EventDayDto) {
    const dialogRef = this.dialog.open(EventEditFormComponent, {
      width: '500px',
      data: {
        id: eventDay.event.id.toString(),
        name: eventDay.event.name,
        startTime: eventDay.startTime.substring(0, 5),
        endTime: eventDay.endTime.substring(0, 5),
        tags: eventDay.event.markers.map(m => m.marker.name).join(', ')
      } as EventData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        eventDay.startTime = result.startTime + ':00';
        eventDay.endTime = result.endTime + ':00';
        eventDay.event.name = result.name;
        eventDay.event.markers = result.tags.split(',').map((tag: string) => ({
          id: 0,
          eventId: eventDay.event.id,
          markerId: 0,
          marker: {
            id: 0,
            name: tag.trim(),
            type: '',
            spaceId: this.timetable!.spaceId
          }
        }));
      }
    });
  }

  deleteEvent(dayIndex: number, eventIndex: number) {
    const day = this.timetable!.days.find(d => d.dayName === this.allWeekDays[dayIndex]);
    if (!day) return;

    day.eventDays.splice(eventIndex, 1);

    // Optional: Remove day from timetable if it has no events
    if (day.eventDays.length === 0) {
      this.timetable!.days = this.timetable!.days.filter(d => d !== day);
      this.connectedDropLists = this.timetable!.days.map((_, i) => `day-${i}`);
    }
  }

  cancelChanges() {
    this.router.navigate(['/schedule']);
  }

  saveChanges() {
    if (!this.timetable) return;

    this.scheduleService.updateTimetable(this.timetable).subscribe({
      next: () => this.router.navigate(['/schedule']),
      error: err => console.error('Error saving timetable', err)
    });
  }

  getEventTime(ed: EventDayDto): string {
    return `${ed.startTime.substring(0, 5)} - ${ed.endTime.substring(0, 5)}`;
  }

  getEventDescription(ed: EventDayDto): string {
    return ed.event.markers.map(m => m.marker.name).join(', ');
  }
}
