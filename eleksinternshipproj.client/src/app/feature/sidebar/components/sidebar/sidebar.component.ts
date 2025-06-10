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
  }
  
}
