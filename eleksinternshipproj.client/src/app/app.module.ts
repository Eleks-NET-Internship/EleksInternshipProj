import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CalendarComponent } from './shared/components/schedule/calendar/calendar.component';
import { CalendarFormComponent } from './shared/components/schedule/calendar-form/calendar-form.component';
import { ScheduleComponent } from './shared/components/schedule/schedule/schedule.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';

// Angular Material
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [
    AppComponent,
    CalendarComponent,
    CalendarFormComponent,
    ScheduleComponent,
    SidebarComponent,
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    // Material modules
    MatSidenavModule,
    MatListModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    FormsModule 
],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
