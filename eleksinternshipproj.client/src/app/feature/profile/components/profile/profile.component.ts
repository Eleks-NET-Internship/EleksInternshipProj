import { Component } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProfileDto, ProfileResponse, UpdateProfileDto } from '../../models/profile-models';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  profileForm: FormGroup;

  email: string = "";

  constructor(private fb: FormBuilder, private profileService: ProfileService, private snackBar: MatSnackBar) {
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
    const formValues = this.profileForm.value;

    const userProfile: UpdateProfileDto = {
      username: formValues.username.trim(),
      firstName: formValues.firstname?.trim() || undefined,
      lastName: formValues.lastname?.trim() || undefined,
    };

    this.profileService.updateProfile(userProfile).subscribe({
      next: (response) => {
        this.snackBar.open('��� ��������!', '�������', {
          duration: 5000,
        });
      },
      error: (error) => {
        console.error(error.error.message);
        this.snackBar.open('�� ������� ������� ��� :(', '�������', {
          duration: 5000,
          panelClass: ['snackbar-error']
        });
      }
    });
  }
}
