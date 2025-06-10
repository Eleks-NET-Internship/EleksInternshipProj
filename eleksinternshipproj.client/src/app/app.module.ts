import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CalendarComponent } from './feature/calendar/components/calendar/calendar.component';
import { CalendarFormComponent } from './feature/calendar/components/calendar-form/calendar-form.component';
import { ScheduleComponent } from './feature/schedule/components/schedule/schedule.component';
import { SidebarComponent } from './feature/sidebar/components/sidebar/sidebar.component';
import { EditScheduleComponent } from './feature/schedule/components/edit-schedule/edit-schedule.component';
import { EventEditFormComponent } from './feature/schedule/components/event-edit-form/event-edit-form.component';

// Angular Material
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRippleModule } from '@angular/material/core';
import { MatChipsModule } from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';





@NgModule({
  declarations: [
    AppComponent,
    CalendarComponent,
    CalendarFormComponent,
    ScheduleComponent,
    SidebarComponent,
    EditScheduleComponent,
    EventEditFormComponent,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    AppRoutingModule,
    // Material modules
    MatSidenavModule,
    MatListModule,
    AppRoutingModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    FormsModule,
    CommonModule,
    MatDialogModule,
    MatToolbarModule,
    MatMenuModule,
    MatTooltipModule,
    MatSnackBarModule, 
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatRippleModule,
    MatChipsModule,
    MatAutocompleteModule,
    // CDK Modules
    DragDropModule,
],
  providers: [
    provideAnimationsAsync(),
    provideHttpClient(withInterceptorsFromDi(), withInterceptors([authInterceptor]))
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
