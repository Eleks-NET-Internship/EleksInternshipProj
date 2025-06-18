// calendar-form.component.ts
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

import type { TaskDTO, UniqueEventDTO } from '../../models/calendar-models';
import { CalendarService } from '../../services/calendar.service';

@Component({
  selector: 'app-calendar-form',
  templateUrl: './calendar-form.component.html',
  styleUrls: ['./calendar-form.component.css']
})
export class CalendarFormComponent implements OnChanges {
  @Input({ required: true }) selectedDate: Date | null = null;

  displayTitle: string = 'СЬОГОДНІ';

  today!: Date;
  
  todayTasks: TaskDTO[] = [];

  tomorrowTasks: TaskDTO[] = [];

  weekTasks: TaskDTO[] = [];

  selectedDateTasks: TaskDTO[] = [];

  events: UniqueEventDTO[] = [];

  constructor(private readonly calendarService: CalendarService) {}

  ngOnChanges(changes: SimpleChanges): void {
    this.loadDisplayData();
    this.updateDisplayTitle();
  }

  private loadDisplayData(): void {
    this.today = new Date();
    this.calendarService.getTasksByDate(this.today).subscribe(tasks => {
      this.todayTasks = tasks;
    });

    const tomorrow = new Date();
    tomorrow.setDate(this.today.getDate() + 1);
    this.calendarService.getTasksByDate(tomorrow).subscribe(tasks => {
      this.tomorrowTasks = tasks;
    });

    this.calendarService.getTasksWithinWeek().subscribe(tasks => {
      this.weekTasks = tasks;
    });

    if (this.selectedDate) {
      this.calendarService.getTasksByDate(this.selectedDate).subscribe(tasks => {
        this.selectedDateTasks = tasks;
      });

      this.calendarService.getEventsByDate(this.selectedDate).subscribe(events => {
        this.events = events;
      });
    }
  }

  // Method to update display data (could be called from parent component)
  updateDisplayData(todayTasks?: TaskDTO[], tomorrowTasks?: TaskDTO[], weekTasks?: TaskDTO[]): void {
    if (todayTasks) {
      this.todayTasks = { ...this.todayTasks, ...todayTasks };
    }
    if (tomorrowTasks) {
      this.tomorrowTasks = { ...this.tomorrowTasks, ...tomorrowTasks };
    }
    if (weekTasks) {
      this.weekTasks = { ...this.weekTasks, ...weekTasks };
    }
  }

  // Helper method to get all tasks as a single object
  getAllTasks(): { today: TaskDTO[], tomorrow: TaskDTO[], week: TaskDTO[] } {
    return {
      today: this.todayTasks,
      tomorrow: this.tomorrowTasks,
      week: this.weekTasks
    };
  }

  private updateDisplayTitle(): void {
    if (!this.selectedDate) {
      this.displayTitle = 'СЬОГОДНІ';
    } else {
      const options: Intl.DateTimeFormatOptions = { day: 'numeric', month: 'long', year: 'numeric' };
      this.displayTitle = this.selectedDate.toLocaleDateString('uk-UA', options);
    }
  }

  getDisplayTitle(): string {
    return this.displayTitle;
  }
}