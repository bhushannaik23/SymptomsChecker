import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { User } from '../../../models/auth.model';

@Component({
  selector: 'app-admin-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class AdminProfileComponent implements OnInit {
  admin: User | null = null;
  profileForm: FormGroup;
  isEditing = false;
  isLoading = false;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.profileForm = this.fb.group({
      name: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.loadProfile();
  }

  loadProfile() {
    this.admin = this.authService.getCurrentUser();
    if (this.admin) {
      this.profileForm.patchValue({
        name: this.admin.name,
      });
    }
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
    if (!this.isEditing && this.admin) {
      this.profileForm.patchValue({
        name: this.admin.name,
      });
    }
  }

  updateProfile() {
    if (this.profileForm.valid) {
      this.isLoading = true;
      // Note: Update admin profile API needs to be implemented in backend
      setTimeout(() => {
        this.isLoading = false;
        this.isEditing = false;
        alert('Profile update functionality needs backend implementation');
      }, 1000);
    }
  }
}
