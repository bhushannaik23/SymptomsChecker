import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AppointmentService } from '../../../services/appointment.service';
import { DoctorService } from '../../../services/doctor.service';
import { AuthService } from '../../../core/services/auth.service';
import { Appointment, CreateAppointmentRequest } from '../../../models/appointment.model';
import { Doctor } from '../../../models/doctor.model';
import { User } from '../../../models/auth.model';

@Component({
  selector: 'app-patient-appointments',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.scss'],
})
export class PatientAppointmentsComponent implements OnInit {
  appointments: Appointment[] = [];
  doctors: Doctor[] = [];
  showBookingForm = false;
  isBooking = false;
  bookingForm: FormGroup;

  constructor(
    private appointmentService: AppointmentService,
    private doctorService: DoctorService,
    private authService: AuthService,
    private fb: FormBuilder
  ) {
    this.bookingForm = this.fb.group({
      doctorId: ['', Validators.required],
      appointmentDate: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.loadAppointments();
    this.loadDoctors();
  }

  loadAppointments() {
    const currentUser: User | null = this.authService.getCurrentUser();
    if (currentUser) {
      this.appointmentService.getPatientAppointments(currentUser.userId).subscribe({
        next: (appointments: Appointment[]) => (this.appointments = appointments),
        error: (error) => console.error('Error loading appointments:', error),
      });
    }
  }

  loadDoctors() {
    this.doctorService.getAllDoctors().subscribe({
      next: (doctors: Doctor[]) => (this.doctors = doctors),
      error: (error) => console.error('Error loading doctors:', error),
    });
  }

  bookAppointment() {
    if (this.bookingForm.valid) {
      this.isBooking = true;
      const currentUser: User | null = this.authService.getCurrentUser();

      if (currentUser) {
        const appointmentData: CreateAppointmentRequest = {
          patientId: currentUser.userId,
          doctorId: this.bookingForm.value.doctorId,
          dateTime: new Date(this.bookingForm.value.appointmentDate)
        };

        this.appointmentService.createAppointment(appointmentData).subscribe({
          next: (result) => {
            this.isBooking = false;
            this.showBookingForm = false;
            this.bookingForm.reset();
            this.loadAppointments();
            alert('Appointment booked successfully!');
          },
          error: (error) => {
            this.isBooking = false;
            console.error('Error booking appointment:', error);
            alert('Failed to book appointment');
          },
        });
      }
    }
  }
}
