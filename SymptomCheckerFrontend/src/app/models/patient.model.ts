export interface Patient {
  patientId: number;
  name: string;
  email: string;
  phone: string;
  dateCreated: Date;
}

export interface CreatePatient {
  name: string;
  email: string;
  password: string;
  phone: string;
}

export interface UpdatePatientRequest {
  name: string;
  phone: string;
}
