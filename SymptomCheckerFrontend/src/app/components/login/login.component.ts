import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { LoginRequest, LoginResponse } from '../../models/auth.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;
  isLoading = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      userType: ['patient', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.isLoading = true;
      const { userType, email, password } = this.loginForm.value;

      const loginRequest: LoginRequest = { email, password, userType };
      this.authService.login(loginRequest).subscribe({
        next: (response: LoginResponse) => {
          this.isLoading = false;
          if (response.userType === 'patient') {
            this.router.navigate(['/patient/dashboard']);
          } else if (response.userType === 'doctor') {
            this.router.navigate(['/doctor/dashboard']);
          } else if (response.userType === 'admin') {
            this.router.navigate(['/admin/dashboard']);
          }
        },
        error: (error) => {
          this.isLoading = false;
          alert('Login failed');
        },
      });
    }
  }
}
