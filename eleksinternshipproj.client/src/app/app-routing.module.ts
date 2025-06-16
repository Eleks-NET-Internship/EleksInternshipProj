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
import { NotesComponent } from './feature/notes/components/notes/notes.component';
import { EventNotesComponent } from './feature/notes/components/event-notes/event-notes.component';
import { NoteDetailComponent } from './feature/notes/components/note-detail/note-detail.component';

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
      { path: 'calendar', component: CalendarComponent, canActivate: [authGuard] },
      { path: 'schedule', component: ScheduleComponent, canActivate: [authGuard] },
      { path: 'notes', component: NotesComponent, canActivate: [authGuard] },
      { path: 'event-notes', component: EventNotesComponent, canActivate: [authGuard] },
      { path: 'notes/:id', component: NoteDetailComponent, canActivate: [authGuard] },
      { path: 'edit-schedule', component: EditScheduleComponent, canActivate: [authGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [authGuard] },
    ]
  },

  { path: '**', redirectTo: '/profile' } // change to space selection page later
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
