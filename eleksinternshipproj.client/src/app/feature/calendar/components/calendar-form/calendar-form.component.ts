// calendar-form.component.ts
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

interface TaskGroup {
  task1?: string;
  task2?: string;
  task3?: string;
  task4?: string;
  task5?: string;
}

@Component({
  selector: 'app-calendar-form',
  templateUrl: './calendar-form.component.html',
  styleUrls: ['./calendar-form.component.css']
})
export class CalendarFormComponent implements OnChanges {
  @Input() selectedDate: Date | null = null; // variable that has the current chosen date, use it for the data fetching from the db

  displayTitle: string = 'СЬОГОДНІ';
  
  todayTasks: TaskGroup = {
    task1: 'тралалейлотралала',
    task2: 'пумпурумпумпум'
  };

  tomorrowTasks: TaskGroup = {
    task1: 'лалалалала'
  };

  weekTasks: TaskGroup = {
    task1: 'тралалейлотралала',
    task2: 'пумпурумпумпум',
    task3: 'лалалалала',
    task4: 'тактовоттактоарешилла',
    task5: 'саміпосудите'
  };

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    // Initialize with default display values
    // In a real application, you might load this data from a service
    this.loadDisplayData();
    this.updateDisplayTitle();
  }

  private loadDisplayData(): void {
    // This method could be used to load data from a service
    // For now, we're using the default values set above
    console.log('Display data loaded');
  }

  // Method to update display data (could be called from parent component)
  updateDisplayData(todayTasks?: TaskGroup, tomorrowTasks?: TaskGroup, weekTasks?: TaskGroup): void {
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
  getAllTasks(): { today: TaskGroup, tomorrow: TaskGroup, week: TaskGroup } {
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