import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class AdminDashboardComponent {
  constructor(private router: Router) {}

  navigateToDoctors() {
    this.router.navigate(['/admin/doctors']);
  }

  navigateToPatients() {
    this.router.navigate(['/admin/patients']);
  }

  navigateToProfile() {
    this.router.navigate(['/admin/profile']);
  }
}
