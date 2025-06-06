import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './feature/login/components/login/login.component';
import { RegisterComponent } from './feature/register/components/register/register.component';
import { WeatherForecastComponent } from './feature/weatherForecast/components/weather-forecast/weather-forecast.component';
import { authGuard } from './core/guards/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forecast', component: WeatherForecastComponent, canActivate: [authGuard] }, // when Home component arrives, switch to it here
  { path: '', component: WeatherForecastComponent, canActivate: [authGuard] }, // and here
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
