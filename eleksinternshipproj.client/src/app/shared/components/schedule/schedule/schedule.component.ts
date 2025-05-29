import { Component } from '@angular/core';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent {
  
  daysOfWeek = ['Понеділок', 'Вівторок', 'Середа', 'Четвер', 'П\'ятниця'];

  // Прості дані для відображення як на картинці
  scheduleData = {
    monday: [
      {
        time: '14:50 - 16:10',
        title: 'Сховища даних',
        hasNotification: false
      },
      {
        time: '16:25 - 17:45',
        title: 'Сховища даних',
        description: '5 д.р. дедлайн: сьогодні',
        hasNotification: true
      }
    ]
  };

  onCardClick(card: any) {
    console.log('Card clicked:', card);
  }
}