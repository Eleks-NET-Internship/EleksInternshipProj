import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SpaceContextService } from '../../../../core/services/space-context/space-context.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  sidenavItems = [
    { icon: 'workspace', label: 'Простір', route: "/spaces" },
    { icon: 'calendar_today', label: 'Календар', route: "/calendar" },
    { icon: 'schedule', label: 'Розклад', route: "/schedule" },
    { icon: 'assignment', label: 'Завдання', route: "/tasks" },
    { icon: 'note', label: 'Нотатки', route: "/notes" },
    { icon: 'event', label: 'Події', route: "/events" },
    //  { icon: 'bar_chart', label: 'Статистика', route: "/statistics" },
    { icon: 'notifications', label: 'Сповіщення', route: "/notifications" },
    { icon: 'person', label: 'Профіль', route: "/profile" },
    { icon: 'settings', label: 'Налаштування', route: "/settings" },
  ].map(item => ({
    ...item,
    active: false,
    disabled: true
  }));

  alwaysEnabled = ["/spaces", "/notifications", "/profile", "/settings"]

  private routerSubscription?: Subscription;
  private spaceSubscription?: Subscription;

  constructor(private router: Router, private spaceContextService: SpaceContextService) { }

  ngOnInit() {
    this.updateActiveItem(this.router.url);

    this.routerSubscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateActiveItem(event.urlAfterRedirects);
      }
    });

    this.spaceSubscription = this.spaceContextService.space$.subscribe(space => {
      if (space) {
        this.setDisabledState(true);
        this.sidenavItems[0].label = this.spaceContextService.getSpaceName()??"";
      } else {
        this.setDisabledState(false);
        this.sidenavItems[0].label = "Простір";
      }
    })
  }

  ngOnDestroy() {
    this.routerSubscription?.unsubscribe();
    this.spaceSubscription?.unsubscribe();
  }

  onMenuClick(clickedItem: any) {
    if (clickedItem.disabled)
      return;
    this.router.navigate([clickedItem.route]);
  }

  private updateActiveItem(url: string) {
    this.sidenavItems = this.sidenavItems.map(item => ({
      ...item,
      active: url.startsWith(item.route)
    }));
  }

  private setDisabledState(spaceSelected: boolean) {
    this.sidenavItems = this.sidenavItems.map(item => ({
      ...item,
      disabled: spaceSelected ? false : !this.alwaysEnabled.includes(item.route)
    }))
  }
}
