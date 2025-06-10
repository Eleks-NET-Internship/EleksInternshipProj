import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { EventEditFormComponent, EventData } from '../event-edit-form/event-edit-form.component';

export interface ScheduleEvent {
  id: string;
  title: string;
  time: string;
  description?: string;
  color?: string;
}

export interface DaySchedule {
  day: string;
  date: string;
  events: ScheduleEvent[];
}

@Component({
  selector: 'app-edit-schedule',
  templateUrl: './edit-schedule.component.html',
  styleUrls: ['./edit-schedule.component.scss']
})
export class EditScheduleComponent implements OnInit {
  
  
  currentWeekStart: Date = new Date();
  
  schedule: DaySchedule[] = [
    {
      day: 'Понеділок',
      date: '14:50 - 16:10',
      events: [
        {
          id: '1',
          title: 'Сховища даних',
          time: '14:50 - 16:10',
          description: '',
          color: '#e3f2fd'
        }
      ]
    },
    {
      day: 'Вівторок',
      date: '',
      events: []
    },
    {
      day: 'Середа',
      date: '',
      events: []
    },
    {
      day: 'Четвер',
      date: '',
      events: []
    },
    {
      day: "П'ятниця",
      date: '',
      events: []
    },
    {
      day: 'Субота',
      date: '16:25 - 17:45',
      events: [
        {
          id: '2',
          title: 'Сховища даних',
          time: '16:25 - 17:45',
          description: '5 ауд. лекційно-сесійний',
          color: '#ffebee'
        }
      ]
    },
    {
      day: 'Неділя',
      date: '',
      events: []
    }
  ];

  connectedDropLists: string[] = [];

  ngOnInit() {
    // Створюємо список усіх drop zones для кожного дня
    this.connectedDropLists = this.schedule.map((_, index) => `day-${index}`);
  }

  get currentWeekEnd(): Date {
    return new Date(this.currentWeekStart.getTime() + 6 * 24 * 60 * 60 * 1000);
  }

  getWeekDateRange(): string {
    return `${this.currentWeekStart.toLocaleDateString('uk-UA')} - ${this.currentWeekEnd.toLocaleDateString('uk-UA')}`;
  }

  onEventDrop(event: CdkDragDrop<ScheduleEvent[]>) {
    if (event.previousContainer === event.container) {
      // Переміщення в межах одного дня
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      // Переміщення між днями
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  addEvent(dayIndex: number) {
    const newEvent: ScheduleEvent = {
      id: Date.now().toString(),
      title: 'Нова подія',
      time: '00:00 - 00:00',
      description: '',
      color: '#f3e5f5'
    };
    this.schedule[dayIndex].events.push(newEvent);
  }

  
  constructor(private dialog: MatDialog) {}
  editEvent(event: ScheduleEvent) {
    const existingEvent: EventData = {
      id: '1',
      name: 'Зустріч з командою',
      startTime: '14:00',
      endTime: '15:30',
      tags: 'робота, зустріч, команда'
    };

    const dialogRef = this.dialog.open(EventEditFormComponent, {
      width: '500px',
      data: existingEvent
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Подія оновлена:', result);
        // Тут можна додати логіку оновлення події
        this.updateEvent(result);
      }
    });
    // Логіка для редагування події
    // console.log('Редагування події:', event);
  }

  private saveEvent(event: EventData): void {
    // Логіка збереження нової події
    console.log('Збереження події:', event);
  }

  private updateEvent(event: EventData): void {
    // Логіка оновлення існуючої події
    console.log('Оновлення події:', event);
  }
  
  deleteEvent(dayIndex: number, eventIndex: number) {
    this.schedule[dayIndex].events.splice(eventIndex, 1);
  }

  cancelChanges() {
    // Логіка для скасування змін
    console.log('Скасування змін');
  }

  saveChanges() {
    // Логіка для збереження змін
    console.log('Збереження змін', this.schedule);
  }

  previousWeek() {
    this.currentWeekStart = new Date(this.currentWeekStart.getTime() - 7 * 24 * 60 * 60 * 1000);
  }

  nextWeek() {
    this.currentWeekStart = new Date(this.currentWeekStart.getTime() + 7 * 24 * 60 * 60 * 1000);
  }
}