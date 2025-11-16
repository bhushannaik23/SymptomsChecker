import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { SymptomAnalysisService } from '../../../services/symptom-analysis.service';
import { DoctorService } from '../../../services/doctor.service';
import { AppointmentService } from '../../../services/appointment.service';
import { AuthService } from '../../../core/services/auth.service';
import {
  SymptomAnalysisRequest,
  SymptomAnalysisResponse,
  RecommendedDoctor,
} from '../../../models/symptom-analysis.models';
import { CreateAppointmentRequest } from '../../../models/appointment.model';
import { User } from '../../../models/auth.model';

@Component({
  selector: 'app-symptoms',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './symptoms.component.html',
  styleUrls: ['./symptoms.component.scss'],
})
export class SymptomsComponent {
  symptomForm: FormGroup;
  isLoading = false;
  analysisResult: SymptomAnalysisResponse | null = null;
  recommendedDoctors: RecommendedDoctor[] = [];

  constructor(
    private fb: FormBuilder,
    private symptomAnalysisService: SymptomAnalysisService,
    private doctorService: DoctorService,
    private appointmentService: AppointmentService,
    private authService: AuthService
  ) {
    this.symptomForm = this.fb.group({
      symptoms: ['', Validators.required],
    });
  }

  analyzeSymptoms() {
    if (this.symptomForm.valid) {
      this.isLoading = true;
      const currentUser: User | null = this.authService.getCurrentUser();

      if (currentUser) {
        const request: SymptomAnalysisRequest = {
          patientId: currentUser.userId,
          symptomsText: this.symptomForm.value.symptoms,
        };

        this.symptomAnalysisService.analyzeSymptoms(request).subscribe({
          next: (result: SymptomAnalysisResponse) => {
            this.analysisResult = result;
            this.recommendedDoctors = result.recommendedDoctors;
            this.isLoading = false;
          },
          error: (error) => {
            console.error('Error analyzing symptoms:', error);
            this.isLoading = false;
          },
        });
      }
    }
  }

  bookAppointment(doctorId: number) {
    const currentUser: User | null = this.authService.getCurrentUser();
    if (currentUser) {
      const appointmentData: CreateAppointmentRequest = {
        patientId: currentUser.userId,
        doctorId: doctorId,
        dateTime: new Date()
      };

      this.appointmentService.createAppointment(appointmentData).subscribe({
        next: (result) => {
          alert('Appointment booked successfully!');
        },
        error: (error) => {
          console.error('Error booking appointment:', error);
          alert('Failed to book appointment');
        },
      });
    }
  }
}
