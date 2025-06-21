import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  sidenavItems = [
    { icon: 'workspace', label: 'Простір', route: "/spaces", active:false },
    { icon: 'calendar_today', label: 'Календар', route: "/calendar", active: false },
    { icon: 'schedule', label: 'Розклад', route: "/schedule", active: false },
    { icon: 'assignment', label: 'Завдання', route: "/tasks", active: false },
    { icon: 'note', label: 'Нотатки', route: "/notes", active: false },
    { icon: 'event', label: 'Події', route: "/events", active: false },
  //  { icon: 'bar_chart', label: 'Статистика', route: "/statistics", active:false },
    { icon: 'notifications', label: 'Сповіщення', route: "/notifications", active: false },
    { icon: 'person', label: 'Профіль', route: "/profile", active: false },
    { icon: 'settings', label: 'Налаштування', route: "/settings", active: false },
  ];

  private routerSubscription?: Subscription;

  constructor(private router: Router) { }

  ngOnInit() {
    this.updateActiveItem(this.router.url);

    this.routerSubscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateActiveItem(event.urlAfterRedirects);
      }
    })
  }

  ngOnDestroy() {
    this.routerSubscription?.unsubscribe();
  }

  onMenuClick(clickedItem: any) {
    this.router.navigate([clickedItem.route]);
  }

  private updateActiveItem(url: string) {
    this.sidenavItems = this.sidenavItems.map(item => ({
      ...item,
      active: url.startsWith(item.route)
    }));
  }
}
