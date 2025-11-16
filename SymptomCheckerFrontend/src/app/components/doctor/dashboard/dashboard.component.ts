import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DoctorDashboardComponent {
  constructor(private router: Router) {}

  navigateToAppointments() {
    this.router.navigate(['/doctor/appointments']);
  }

  navigateToProfile() {
    this.router.navigate(['/doctor/profile']);
  }
}
