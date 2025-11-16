import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { PatientService } from '../../../services/patient.service';
import { AuthService } from '../../../core/services/auth.service';
import { Patient, UpdatePatientRequest } from '../../../models/patient.model';
import { User } from '../../../models/auth.model';

@Component({
  selector: 'app-patient-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class PatientProfileComponent implements OnInit {
  profileForm: FormGroup;
  isUpdating = false;

  constructor(
    private fb: FormBuilder,
    private patientService: PatientService,
    private authService: AuthService
  ) {
    this.profileForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.loadProfile();
  }

  loadProfile() {
    const currentUser: User | null = this.authService.getCurrentUser();
    if (currentUser) {
      this.patientService.getPatientById(currentUser.userId).subscribe({
        next: (patient: Patient) => {
          this.profileForm.patchValue({
            name: patient.name,
            email: patient.email,
            phone: patient.phone
          });
        },
        error: (error) => console.error('Error loading profile:', error)
      });
    }
  }

  updateProfile() {
    if (this.profileForm.valid) {
      this.isUpdating = true;
      const currentUser: User | null = this.authService.getCurrentUser();
      
      if (currentUser) {
        const updateData: UpdatePatientRequest = {
          name: this.profileForm.value.name,
          phone: this.profileForm.value.phone
        };
        
        this.patientService.updatePatient(currentUser.userId, updateData).subscribe({
          next: (result) => {
            this.isUpdating = false;
            alert('Profile updated successfully!');
          },
          error: (error) => {
            this.isUpdating = false;
            console.error('Error updating profile:', error);
            alert('Failed to update profile');
          }
        });
      }
    }
  }
}