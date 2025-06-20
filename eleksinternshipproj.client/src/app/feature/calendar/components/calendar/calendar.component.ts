import { Component, signal, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddEventComponent } from '../add-event/add-event.component';

import type { AddUniqueEventDto } from '../../models/calendar-models'; 
import { CalendarService } from '../../services/calendar.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  calendarDays: number[] = [];
  selectedDate: { day: number, month: number, year: number } | null = null;
  selectedDateSignal = signal<Date | null>(null);
  currentMonth: number = new Date().getMonth();
  currentYear: number = new Date().getFullYear();

  triggerForForm!: boolean;

  monthNames: string[] = [
    'СІЧЕНЬ', 'ЛЮТИЙ', 'БЕРЕЗЕНЬ', 'КВІТЕНЬ', 'ТРАВЕНЬ', 'ЧЕРВЕНЬ',
    'ЛИПЕНЬ', 'СЕРПЕНЬ', 'ВЕРЕСЕНЬ', 'ЖОВТЕНЬ', 'ЛИСТОПАД', 'ГРУДЕНЬ'
  ];

  constructor( private readonly calendarService: CalendarService, private readonly dialog: MatDialog ) {}

  ngOnInit(): void {
    this.generateCalendar();
  }

  generateCalendar(): void {
    const firstDayOfMonth = new Date(this.currentYear, this.currentMonth, 1);
    const lastDayOfMonth = new Date(this.currentYear, this.currentMonth + 1, 0);
    const daysInMonth = lastDayOfMonth.getDate();

    let firstDayWeek = firstDayOfMonth.getDay();
    firstDayWeek = firstDayWeek === 0 ? 7 : firstDayWeek;

    this.calendarDays = [];

    for (let i = 1; i < firstDayWeek; i++) {
      this.calendarDays.push(0);
    }

    for (let day = 1; day <= daysInMonth; day++) {
      this.calendarDays.push(day);
    }

    const totalCells = 35;
    while (this.calendarDays.length < totalCells) {
      this.calendarDays.push(0);
    }
  }

  selectDay(day: number): void {
    if (day > 0) {
      if (
        this.selectedDate &&
        this.selectedDate.day === day &&
        this.selectedDate.month === this.currentMonth &&
        this.selectedDate.year === this.currentYear
      ) {
        this.selectedDate = null;
        this.selectedDateSignal.set(null);
        console.log(this.selectedDateSignal());//REMOVE AFTER DEBUG
      } else {
        this.selectedDate = {
          day: day,
          month: this.currentMonth,
          year: this.currentYear
        };
        this.selectedDateSignal.set(new Date(this.selectedDate.year, this.selectedDate.month, this.selectedDate.day));
        console.log(this.selectedDateSignal());//REMOVE AFTER DEBUG
      }

      console.log(
        this.selectedDate
          ? `Selected: ${this.selectedDate.day}/${this.selectedDate.month + 1}/${this.selectedDate.year}`
          : 'No selected date'
      );
    }
  }

  isSelectedDay(day: number): boolean {
    if (day === 0 || !this.selectedDate) return false;

    return (
      this.selectedDate.day === day &&
      this.selectedDate.month === this.currentMonth &&
      this.selectedDate.year === this.currentYear
    );
  }

  getCurrentMonthName(): string {
    return this.monthNames[this.currentMonth];
  }

  previousMonth(): void {
    if (this.currentMonth === 0) {
      this.currentMonth = 11;
      this.currentYear--;
    } else {
      this.currentMonth--;
    }

    this.generateCalendar();
  }

  nextMonth(): void {
    if (this.currentMonth === 11) {
      this.currentMonth = 0;
      this.currentYear++;
    } else {
      this.currentMonth++;
    }

    this.generateCalendar();
  }

  onEventCreating() {
    const dialogRef = this.dialog.open(AddEventComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe({
      next: (result: { eventName: string, eventTime: string }) => {
        const addedEvent: AddUniqueEventDto = {
          id: 0,
          eventName: result.eventName,
          eventTime: this.getFullDate(this.selectedDateSignal() as Date, result.eventTime),
          spaceId: 0,
        };
        this.calendarService.addUniqueEvent(addedEvent);
        this.triggerForForm = !this.triggerForForm;
      },
      error: (err) => {
        console.error('Error during event creating: ', err);
      }
    });
  }

  private getFullDate(date: Date, time: string): string {
    const [hours, minutes] = time.split(':').map(Number);
    const timeAsDate = new Date();
    timeAsDate.setHours(hours, minutes, 0, 0);

    const fullDate = new Date(date);
    fullDate.setHours(timeAsDate.getHours());
    fullDate.setMinutes(timeAsDate.getMinutes());
    fullDate.setSeconds(timeAsDate.getSeconds());
    fullDate.setMilliseconds(timeAsDate.getMilliseconds());

    return fullDate.toISOString();
  }

  // private toLocalISOString(date: Date): string {
  //   const pad = (n: number) => String(n).padStart(2, '0');
  //   return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}T` +
  //         `${pad(date.getHours())}:${pad(date.getMinutes())}:${pad(date.getSeconds())}`;
  // }
}