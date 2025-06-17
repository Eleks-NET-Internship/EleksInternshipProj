import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  sidenavItems = [
    { icon: 'workspace', label: 'Простір', active: true },
    { icon: 'calendar_today', label: 'Календар' },
    { icon: 'schedule', label: 'Розклад' },
    { icon: 'assignment', label: 'Завдання' },
    { icon: 'note', label: 'Нотатки' },
    { icon: 'event', label: 'Події' },
    { icon: 'bar_chart', label: 'Статистика' },
    { icon: 'notifications', label: 'Сповіщення' },
    { icon: 'person', label: 'Профіль' },
    { icon: 'settings', label: 'Налаштування' },
  ];

  constructor(private router: Router) { }

  onMenuClick(clickedItem: any) {

    this.sidenavItems = this.sidenavItems.map(item => ({
      ...item,
      active: item.label === clickedItem.label
    }));

    switch (clickedItem.label) {

      case 'Простір': {
        this.router.navigate(['/spaces']);
        break;
      }

      case 'Календар': {
        this.router.navigate(['/calendar']);
        break;
      }

      case 'Розклад': {
        this.router.navigate(['/schedule']);
        break;
      }

      case 'Завдання': {
        this.router.navigate(['/tasks']);
        break;
      }

      case 'Нотатки': {
        this.router.navigate(['/notes']);
        break;
      }

      case 'Події': {
        this.router.navigate(['/events']);
        break;
      }

      case 'Статистика': {
        this.router.navigate(['/statistics']);
        break;
      }

      case 'Сповіщення': {
        this.router.navigate(['/notifications']);
        break;
      }

      case 'Профіль': {
        this.router.navigate(['/profile']);
        break;
      }

      case 'Налаштування': {
        this.router.navigate(['/settings']);
        break;
      }
    }
  }

}
