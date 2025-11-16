import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from '../../shared/header.component';
import { DoctorService } from '../../../services/doctor.service';
import { SpecialtyService } from '../../../core/services/specialty.service';
import { AuthService } from '../../../core/services/auth.service';
import { Doctor, UpdateDoctorRequest } from '../../../models/doctor.model';
import { Specialty } from '../../../models/specialty.model';
import { User } from '../../../models/auth.model';

@Component({
  selector: 'app-doctor-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class DoctorProfileComponent implements OnInit {
  doctor: Doctor | null = null;
  profileForm: FormGroup;
  isEditing = false;
  isLoading = false;
  specialties: Specialty[] = [];
  selectedSpecialties: number[] = [];

  constructor(
    private fb: FormBuilder,
    private doctorService: DoctorService,
    private specialtyService: SpecialtyService,
    private authService: AuthService
  ) {
    this.profileForm = this.fb.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.loadProfile();
    this.loadSpecialties();
  }

  loadSpecialties() {
    this.specialtyService.getSpecialties().subscribe({
      next: (specialties: Specialty[]) => (this.specialties = specialties),
      error: (error) => console.error('Error loading specialties:', error),
    });
  }

  onSpecialtyChange(specialtyId: number, event: any) {
    if (event.target.checked) {
      this.selectedSpecialties.push(specialtyId);
    } else {
      this.selectedSpecialties = this.selectedSpecialties.filter((id) => id !== specialtyId);
    }
  }

  loadProfile() {
    const currentUser: User | null = this.authService.getCurrentUser();
    if (currentUser) {
      this.doctorService.getDoctorById(currentUser.userId).subscribe({
        next: (doctor: Doctor) => {
          this.doctor = doctor;
          this.profileForm.patchValue({
            name: doctor.name,
            phone: doctor.phone,
          });
        },
        error: (error) => console.error('Error loading profile:', error),
      });
    }
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
    if (!this.isEditing && this.doctor) {
      this.profileForm.patchValue({
        name: this.doctor.name,
        phone: this.doctor.phone,
      });
      this.selectedSpecialties = [];
    }
  }

  updateProfile() {
    if (this.profileForm.valid && this.doctor) {
      this.isLoading = true;
      const updateData: UpdateDoctorRequest = {
        name: this.profileForm.value.name,
        phone: this.profileForm.value.phone,
        specialtyIds: this.selectedSpecialties,
      };

      this.doctorService.updateDoctor(this.doctor.doctorId, updateData).subscribe({
        next: (updatedDoctor: Doctor) => {
          this.doctor = updatedDoctor;
          this.isLoading = false;
          this.isEditing = false;
          alert('Profile updated successfully!');
        },
        error: (error) => {
          this.isLoading = false;
          console.error('Error updating profile:', error);
          alert('Failed to update profile');
        },
      });
    }
  }
}
