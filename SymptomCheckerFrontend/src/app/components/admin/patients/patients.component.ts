import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { PatientService } from '../../../services/patient.service';
import { Patient, UpdatePatientRequest } from '../../../models/patient.model';

@Component({
  selector: 'app-admin-patients',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss'],
})
export class AdminPatientsComponent implements OnInit {
  patients: Patient[] = [];
  showAddForm = false;
  editingPatient: Patient | null = null;
  patientForm: FormGroup;
  isLoading = false;

  constructor(private fb: FormBuilder, private patientService: PatientService) {
    this.patientForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phone: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.loadPatients();
  }

  loadPatients() {
    this.patientService.getAllPatients().subscribe({
      next: (patients: Patient[]) => (this.patients = patients),
      error: (error) => console.error('Error loading patients:', error),
    });
  }

  showAdd() {
    this.showAddForm = true;
    this.editingPatient = null;
    this.patientForm.reset();
    this.patientForm.get('password')?.setValidators([Validators.required]);
  }

  editPatient(patient: Patient) {
    this.editingPatient = patient;
    this.showAddForm = true;
    this.patientForm.patchValue({
      name: patient.name,
      email: patient.email,
      phone: patient.phone,
    });
    this.patientForm.get('password')?.clearValidators();
    this.patientForm.get('password')?.updateValueAndValidity();
  }

  savePatient() {
    if (this.patientForm.valid) {
      this.isLoading = true;

      if (this.editingPatient) {
        const updateData: UpdatePatientRequest = {
          name: this.patientForm.value.name,
          phone: this.patientForm.value.phone,
        };

        this.patientService.updatePatient(this.editingPatient.patientId, updateData).subscribe({
          next: () => {
            this.isLoading = false;
            this.cancelForm();
            this.loadPatients();
            alert('Patient updated successfully!');
          },
          error: (error) => {
            this.isLoading = false;
            alert('Failed to update patient');
          },
        });
      } else {
        // Note: Create patient API needs to be implemented in backend
        alert('Create patient functionality needs backend implementation');
        this.isLoading = false;
      }
    }
  }

  deletePatient(patientId: number) {
    if (confirm('Are you sure you want to delete this patient?')) {
      this.patientService.deletePatient(patientId).subscribe({
        next: () => {
          this.loadPatients();
          alert('Patient deleted successfully!');
        },
        error: (error) => {
          alert('Failed to delete patient');
        }
      });
    }
  }

  cancelForm() {
    this.showAddForm = false;
    this.editingPatient = null;
    this.patientForm.reset();
  }
}
