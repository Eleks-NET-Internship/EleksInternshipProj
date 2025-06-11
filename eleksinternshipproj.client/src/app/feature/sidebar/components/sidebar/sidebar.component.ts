import { Component } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  sidenavItems = [
    { icon: 'workspace', label: 'Простір' },
    { icon: 'calendar_today', label: 'Календар' },
    { icon: 'schedule', label: 'Розклад', active: true },
    { icon: 'assignment', label: 'Завдання' },
    { icon: 'note', label: 'Нотатки' },
    { icon: 'event', label: 'Події' },
    { icon: 'bar_chart', label: 'Статистика' },
    { icon: 'person', label: 'Профіль' },
    { icon: 'settings', label: 'Налаштування' }
  ];

  onMenuClick(item: any) {
    console.log('Clicked:', item.label);
    switch (item.label) {

      case 'Простір': {
        console.log('Простір');
        break;
      }

      case 'Календар': {
        console.log('Календар');
        break;
      }

      case 'Розклад': {
        console.log('Розклад');
        break;
      }

      case 'Завдання': {
        console.log('Завдання');
        break;
      }

      case 'Нотатки': {
        console.log('Нотатки');
        break;
      }

      case 'Події': {
        console.log('Події');
        break;
      }

      case 'Статистика': {
        console.log('Статистика');
        break;
      }

      case 'Профіль': {
        console.log('Профіль');
        break;
      }

      case 'Налаштування': {
        console.log('Налаштування');
        break;
      }
    }
  }

}
