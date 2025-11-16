import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { SpecialtyService } from '../../core/services/specialty.service';
import { RegisterPatientRequest, RegisterDoctorRequest, RegisterAdminRequest, LoginResponse } from '../../models/auth.model';
import { Specialty } from '../../models/specialty.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm: FormGroup;
  isLoading = false;
  selectedRole = 'patient';
  specialties: Specialty[] = [];
  selectedSpecialties: number[] = [];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private specialtyService: SpecialtyService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      role: ['patient', Validators.required],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phone: ['', Validators.required]
    });
    this.loadSpecialties();
  }

  onRoleChange() {
    this.selectedRole = this.registerForm.get('role')?.value;
    this.selectedSpecialties = [];
    
    // Update phone field validation based on role
    const phoneControl = this.registerForm.get('phone');
    if (this.selectedRole === 'admin') {
      phoneControl?.clearValidators();
    } else {
      phoneControl?.setValidators([Validators.required]);
    }
    phoneControl?.updateValueAndValidity();
  }

  onSpecialtyChange(specialtyId: number, event: any) {
    console.log('Specialty change:', specialtyId, event.target.checked);
    if (event.target.checked) {
      this.selectedSpecialties.push(specialtyId);
    } else {
      this.selectedSpecialties = this.selectedSpecialties.filter(id => id !== specialtyId);
    }
    console.log('Selected specialties:', this.selectedSpecialties);
  }

  loadSpecialties() {
    this.specialtyService.getSpecialties().subscribe({
      next: (specialties: Specialty[]) => {
        this.specialties = specialties;
        console.log('Loaded specialties:', specialties);
      },
      error: (error) => console.error('Error loading specialties:', error)
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.isLoading = true;
      const formData = this.registerForm.value;
      
      if (formData.role === 'patient') {
        const request: RegisterPatientRequest = {
          name: formData.name,
          email: formData.email,
          password: formData.password,
          phone: formData.phone
        };
        this.authService.registerPatient(request).subscribe({
          next: (response: LoginResponse) => {
            this.isLoading = false;
            alert('Registration successful!');
            this.router.navigate(['/login']);
          },
          error: (error) => {
            this.isLoading = false;
            alert('Registration failed');
          }
        });
      } else if (formData.role === 'doctor') {
        if (this.selectedSpecialties.length === 0) {
          alert('Please select at least one specialty');
          this.isLoading = false;
          return;
        }
        const request: RegisterDoctorRequest = {
          name: formData.name,
          email: formData.email,
          password: formData.password,
          phone: formData.phone,
          specialtyIds: this.selectedSpecialties
        };
        this.authService.registerDoctor(request).subscribe({
          next: (response: LoginResponse) => {
            this.isLoading = false;
            alert('Registration successful!');
            this.router.navigate(['/login']);
          },
          error: (error) => {
            this.isLoading = false;
            alert('Registration failed');
          }
        });
      } else {
        const request: RegisterAdminRequest = {
          name: formData.name,
          email: formData.email,
          password: formData.password
        };
        this.authService.registerAdmin(request).subscribe({
          next: (response: LoginResponse) => {
            this.isLoading = false;
            alert('Registration successful!');
            this.router.navigate(['/login']);
          },
          error: (error) => {
            this.isLoading = false;
            alert('Registration failed');
          }
        });
      }
    }
  }
}