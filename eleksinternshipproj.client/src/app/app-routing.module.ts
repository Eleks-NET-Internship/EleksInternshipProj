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

const routes: Routes = [

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
      { path: 'calendar', component: CalendarComponent, canActivate: [authGuard] },
      { path: 'schedule', component: ScheduleComponent, canActivate: [authGuard] },
      { path: 'edit-schedule', component: EditScheduleComponent, canActivate: [authGuard] },
      { path: 'tasks', component: TasksComponent, canActivate: [authGuard] },
      { path: 'notes', component: NotesComponent, canActivate: [authGuard] },
      { path: 'events', component: EventsComponent, canActivate: [authGuard] },
      { path: 'statistics', component: StatisticsComponent, canActivate: [authGuard] },
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
