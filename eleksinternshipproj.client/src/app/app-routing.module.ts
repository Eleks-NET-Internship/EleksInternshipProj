import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './feature/login/components/login/login.component';
import { RegisterComponent } from './feature/register/components/register/register.component';
import { WeatherForecastComponent } from './feature/weatherForecast/components/weather-forecast/weather-forecast.component';
import { authGuard } from './core/guards/auth.guard';
import { AuthCallbackComponent } from './feature/login/components/auth-callback/auth-callback.component';
import { CalendarComponent } from './feature/calendar/components/calendar/calendar.component';
import { ScheduleComponent } from './feature/schedule/components/schedule/schedule.component';
import { EditScheduleComponent } from './feature/schedule/components/edit-schedule/edit-schedule.component';

const routes: Routes = [
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'calendar', component: CalendarComponent },
  { path: 'schedule', component: ScheduleComponent },
  { path: 'edit-schedule', component: EditScheduleComponent },
  { path: 'forecast', component: WeatherForecastComponent, canActivate: [authGuard] }, // when Home component arrives, switch to it here
  { path: '', component: WeatherForecastComponent, canActivate: [authGuard] }, // and here
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
