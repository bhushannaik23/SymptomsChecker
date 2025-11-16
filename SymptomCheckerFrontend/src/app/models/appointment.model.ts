export interface Appointment {
  appointmentId: number;
  patientId: number;
  patientName: string;
  doctorId: number;
  doctorName: string;
  dateTime: Date;
  status: string;
}

export interface CreateAppointmentRequest {
  patientId: number;
  doctorId: number;
  dateTime: Date;
}

export interface UpdateAppointmentStatusRequest {
  status: string;
}
