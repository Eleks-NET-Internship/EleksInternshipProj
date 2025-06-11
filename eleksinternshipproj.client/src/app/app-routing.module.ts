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

const routes: Routes = [
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'calendar', component: CalendarComponent, canActivate: [authGuard] },
  { path: 'schedule', component: ScheduleComponent, canActivate: [authGuard] },
  { path: 'edit-schedule', component: EditScheduleComponent, canActivate: [authGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [authGuard] },

  { path: '', component: ProfileComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
