import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  sidenavItems = [
    { icon: 'workspace', label: 'Простір' },
    { icon: 'calendar_today', label: 'Календар' },
    { icon: 'schedule', label: 'Розклад' },
    { icon: 'assignment', label: 'Завдання' },
    { icon: 'note', label: 'Нотатки' },
    { icon: 'event', label: 'Події' },
    { icon: 'bar_chart', label: 'Статистика' },
    { icon: 'person', label: 'Профіль', active: true },
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
        console.log('Простір');
        alert("Not implemented :("); // no component for space selection
        break;
      }

      case 'Календар': {
        console.log('Календар');
        this.router.navigate(['/calendar']);
        break;
      }

      case 'Розклад': {
        console.log('Розклад');
        this.router.navigate(['/schedule']);
        break;
      }

      case 'Завдання': {
        console.log('Завдання'); // same
        alert("Not implemented :(");
        break;
      }

      case 'Нотатки': {
        console.log('Нотатки'); // same
        this.router.navigate(['/notes']);
        break;
      }

      case 'Події': {
        console.log('Події'); // same
        alert("Not implemented :(");
        break;
      }

      case 'Статистика': {
        console.log('Статистика'); // same
        alert("Not implemented :(");
        break;
      }

      case 'Профіль': {
        console.log('Профіль');
        this.router.navigate(['/profile']);
        break;
      }

      case 'Налаштування': {
        console.log('Налаштування'); // same
        alert("Not implemented :(");
        break;
      }
    }
  }

}
