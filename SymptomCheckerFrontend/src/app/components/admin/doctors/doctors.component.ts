import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { DoctorService } from '../../../services/doctor.service';
import { SpecialtyService } from '../../../core/services/specialty.service';
import { Doctor, CreateDoctorRequest, UpdateDoctorRequest } from '../../../models/doctor.model';
import { Specialty } from '../../../models/specialty.model';

@Component({
  selector: 'app-admin-doctors',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.scss'],
})
export class AdminDoctorsComponent implements OnInit {
  doctors: Doctor[] = [];
  specialties: Specialty[] = [];
  showAddForm = false;
  editingDoctor: Doctor | null = null;
  doctorForm: FormGroup;
  isLoading = false;
  selectedSpecialties: number[] = [];

  constructor(
    private fb: FormBuilder,
    private doctorService: DoctorService,
    private specialtyService: SpecialtyService
  ) {
    this.doctorForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phone: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.loadDoctors();
    this.loadSpecialties();
  }

  loadDoctors() {
    this.doctorService.getAllDoctors().subscribe({
      next: (doctors: Doctor[]) => (this.doctors = doctors),
      error: (error) => console.error('Error loading doctors:', error),
    });
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

  showAdd() {
    this.showAddForm = true;
    this.editingDoctor = null;
    this.selectedSpecialties = [];
    this.doctorForm.reset();
    this.doctorForm.get('password')?.setValidators([Validators.required]);
  }

  editDoctor(doctor: Doctor) {
    this.editingDoctor = doctor;
    this.showAddForm = true;
    this.selectedSpecialties = [];
    this.doctorForm.patchValue({
      name: doctor.name,
      email: doctor.email,
      phone: doctor.phone,
    });
    this.doctorForm.get('password')?.clearValidators();
    this.doctorForm.get('password')?.updateValueAndValidity();
  }

  saveDoctor() {
    if (this.doctorForm.valid && this.selectedSpecialties.length > 0) {
      this.isLoading = true;

      if (this.editingDoctor) {
        const updateData: UpdateDoctorRequest = {
          name: this.doctorForm.value.name,
          phone: this.doctorForm.value.phone,
          specialtyIds: this.selectedSpecialties,
        };

        this.doctorService.updateDoctor(this.editingDoctor.doctorId, updateData).subscribe({
          next: () => {
            this.isLoading = false;
            this.cancelForm();
            this.loadDoctors();
            alert('Doctor updated successfully!');
          },
          error: (error) => {
            this.isLoading = false;
            alert('Failed to update doctor');
          },
        });
      } else {
        const createData: CreateDoctorRequest = {
          name: this.doctorForm.value.name,
          email: this.doctorForm.value.email,
          password: this.doctorForm.value.password,
          phone: this.doctorForm.value.phone,
        };

        // Note: Create doctor API needs to be implemented in backend
        alert('Create doctor functionality needs backend implementation');
        this.isLoading = false;
      }
    } else {
      alert('Please fill all fields and select at least one specialty');
    }
  }

  deleteDoctor(doctorId: number) {
    if (confirm('Are you sure you want to delete this doctor?')) {
      // Note: Delete doctor API needs to be implemented in backend
      alert('Delete doctor functionality needs backend implementation');
    }
  }

  cancelForm() {
    this.showAddForm = false;
    this.editingDoctor = null;
    this.selectedSpecialties = [];
    this.doctorForm.reset();
  }
}
