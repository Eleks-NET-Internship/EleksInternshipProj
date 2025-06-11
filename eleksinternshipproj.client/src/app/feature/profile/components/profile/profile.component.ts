import { Component } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProfileDto, ProfileResponse } from '../../models/profile-models';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  profileForm: FormGroup;

  email: string = "";

  constructor(private fb: FormBuilder, private profileService: ProfileService) {
    this.profileForm = this.fb.group({
      username: ["", Validators.required],
      firstname: [""],
      lastname: [""]
    });}

  ngOnInit(): void {
    this.getProfile();
  }

  getProfile(): void {
    this.profileService.getProfile().subscribe({
      next: (response: ProfileResponse) => {

        this.profileForm.patchValue({
          username: response.data.username,
          firstname: response.data.firstName,
          lastname: response.data.lastName
        });
        this.email = response.data.email;
      },
      error: (error) => {
        console.error(error.error.message);
      }
    });
  }

  onSave(): void {
    // do nothing at all
  }
}
