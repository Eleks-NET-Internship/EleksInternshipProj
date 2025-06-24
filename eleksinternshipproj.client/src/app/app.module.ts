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
import { EventEditFormComponent } from './feature/schedule/components/event-edit-form/event-edit-form.component'
import { LoginComponent } from './feature/login/components/login/login.component';
import { RegisterComponent } from './feature/register/components/register/register.component';
import { AddEventComponent } from './feature/calendar/components/add-event/add-event.component';
import { authInterceptor } from './core/interceptors/auth.interceptor';

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
import { ProfileComponent } from './feature/profile/components/profile/profile.component';
import { SharedModule } from './shared/shared.module';
import { SettingsComponent } from './feature/settings/components/settings/settings.component';
import { StatisticsComponent } from './feature/statistics/components/statistics/statistics.component';
import { SpacesComponent } from './feature/spaces/components/spaces/spaces.component';
import { NotesComponent } from './feature/notes/components/notes/notes.component';
import { EventsComponent } from './feature/events/components/events/events.component';
import { TasksComponent } from './feature/tasks/components/tasks/tasks.component';
import { AddEventDialogComponent } from './feature/events/components/add-event-dialog/add-event-dialog.component';
import { EventDetailComponent } from './feature/events/components/event-detail/event-detail.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NotificationsComponent } from './feature/notifications/components/notifications/notifications.component';
import { TaskDetailsDialogComponent } from './feature/tasks/components/task-details-dialog/task-details-dialog.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { AddTaskDialogComponent } from './feature/tasks/components/add-task-dialog/add-task-dialog.component';
import { EventComponent } from './feature/calendar/components/event/event.component'; 

import { EventNotesComponent } from './feature/notes/components/event-notes/event-notes.component';
import { NoteDetailComponent } from './feature/notes/components/note-detail/note-detail.component';
import { MatSelectModule } from '@angular/material/select'; // Додайте цей імпорт
import { MatOptionModule } from '@angular/material/core'; // Додайте цей імпорт
import { NotifySpaceComponent } from './feature/notifications/components/notify-space/notify-space.component'; 



@NgModule({
  declarations: [
    AppComponent,
    CalendarComponent,
    CalendarFormComponent,
    ScheduleComponent,
    EditScheduleComponent,
    EventEditFormComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    SettingsComponent,
    StatisticsComponent,
    SpacesComponent,
    NotesComponent,
    EventsComponent,
    TasksComponent,
    AddEventDialogComponent,
    EventDetailComponent,
    NotificationsComponent,
    AddEventComponent,
    TaskDetailsDialogComponent,
    AddTaskDialogComponent,
    EventNotesComponent,
    NoteDetailComponent,
    NotifySpaceComponent,
    EventComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    // Material modules
    MatSelectModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
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
    MatProgressSpinnerModule,
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
