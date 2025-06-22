import { Component } from '@angular/core';
import { SettingsService } from '../../services/settings.service';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {

  constructor(private settingsService: SettingsService, private authService: AuthService) { }

  logOut(): void {
    this.authService.logOut();
  }
}
