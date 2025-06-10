import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  calendarDays: number[] = [];
  selectedDate: { day: number, month: number, year: number } | null = null;
  currentMonth: number = new Date().getMonth();
  currentYear: number = new Date().getFullYear();

  monthNames: string[] = [
    'СІЧЕНЬ', 'ЛЮТИЙ', 'БЕРЕЗЕНЬ', 'КВІТЕНЬ', 'ТРАВЕНЬ', 'ЧЕРВЕНЬ',
    'ЛИПЕНЬ', 'СЕРПЕНЬ', 'ВЕРЕСЕНЬ', 'ЖОВТЕНЬ', 'ЛИСТОПАД', 'ГРУДЕНЬ'
  ];

  constructor() {}

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
      } else {
        this.selectedDate = {
          day: day,
          month: this.currentMonth,
          year: this.currentYear
        };
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

  getSelectedDate(): Date | null {
    return this.selectedDate
      ? new Date(this.selectedDate.year, this.selectedDate.month, this.selectedDate.day)
      : null;
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
}