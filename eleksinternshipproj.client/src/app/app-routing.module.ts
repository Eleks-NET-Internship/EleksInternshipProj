import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './feature/login/components/login/login.component';
import { RegisterComponent } from './feature/register/components/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { AuthCallbackComponent } from './feature/login/components/auth-callback/auth-callback.component';
import { CalendarComponent } from './feature/calendar/components/calendar/calendar.component';
import { ScheduleComponent } from './feature/schedule/components/schedule/schedule.component';
import { EditScheduleComponent } from './feature/schedule/components/edit-schedule/edit-schedule.component';
import { ProfileComponent } from './feature/profile/components/profile/profile.component';
import { MainLayoutComponent } from './shared/components/layouts/main-layout/main-layout.component';
import { AuthLayoutComponent } from './shared/components/layouts/auth-layout/auth-layout.component';
import { SettingsComponent } from './feature/settings/components/settings/settings.component';
import { StatisticsComponent } from './feature/statistics/components/statistics/statistics.component';
import { TasksComponent } from './feature/tasks/components/tasks/tasks.component';
import { NotesComponent } from './feature/notes/components/notes/notes.component';
import { EventsComponent } from './feature/events/components/events/events.component';
import { SpacesComponent } from './feature/spaces/components/spaces/spaces.component';
import { EventDetailComponent } from './feature/events/components/event-detail/event-detail.component';
import { NotificationsComponent } from './feature/notifications/components/notifications/notifications.component';
import { spaceContextGuard } from './core/guards/space-context.guard';

const routes: Routes = [

  { path: '', redirectTo: '/login', pathMatch: 'full' },

  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: 'auth-callback', component: AuthCallbackComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
    ]
  },

  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: 'spaces', component: SpacesComponent, canActivate: [authGuard] },
      { path: 'calendar', component: CalendarComponent, canActivate: [authGuard, spaceContextGuard] },
      { path: 'schedule', component: ScheduleComponent, canActivate: [authGuard, spaceContextGuard] },
      { path: 'edit-schedule', component: EditScheduleComponent, canActivate: [authGuard, spaceContextGuard] },
      { path: 'tasks', component: TasksComponent, canActivate: [authGuard, spaceContextGuard] },
      { path: 'notes', component: NotesComponent, canActivate: [authGuard, spaceContextGuard] },
      { path: 'events', component: EventsComponent, canActivate: [authGuard, spaceContextGuard] },
      { path: 'event/:id', component: EventDetailComponent, canActivate: [authGuard, spaceContextGuard] },
      //{ path: 'statistics', component: StatisticsComponent, canActivate: [authGuard, spaceContextGuard] },
      { path: 'notifications', component: NotificationsComponent, canActivate: [authGuard, spaceContextGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [authGuard] },
      { path: 'settings', component: SettingsComponent, canActivate: [authGuard] }
    ]
  },

  { path: '**', redirectTo: '/spaces' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
