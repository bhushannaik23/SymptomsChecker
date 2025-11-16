import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppointmentService } from '../../../services/appointment.service';
import { AuthService } from '../../../core/services/auth.service';
import { Appointment, UpdateAppointmentStatusRequest } from '../../../models/appointment.model';
import { User } from '../../../models/auth.model';

@Component({
  selector: 'app-doctor-appointments',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.scss'],
})
export class DoctorAppointmentsComponent implements OnInit {
  appointments: Appointment[] = [];
  isUpdating = false;

  constructor(private appointmentService: AppointmentService, private authService: AuthService) {}

  ngOnInit() {
    this.loadAppointments();
  }

  loadAppointments() {
    const currentUser: User | null = this.authService.getCurrentUser();
    if (currentUser) {
      this.appointmentService.getDoctorAppointments(currentUser.userId).subscribe({
        next: (appointments: Appointment[]) => (this.appointments = appointments),
        error: (error) => console.error('Error loading appointments:', error),
      });
    }
  }

  updateAppointmentStatus(appointmentId: number, status: string) {
    this.isUpdating = true;
    const updateData: UpdateAppointmentStatusRequest = { status };

    this.appointmentService.updateAppointmentStatus(appointmentId, updateData).subscribe({
      next: () => {
        this.isUpdating = false;
        this.loadAppointments();
        alert('Appointment status updated successfully!');
      },
      error: (error) => {
        this.isUpdating = false;
        console.error('Error updating appointment:', error);
        alert('Failed to update appointment status');
      },
    });
  }
}
