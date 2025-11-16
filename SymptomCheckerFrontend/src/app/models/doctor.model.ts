export interface Doctor {
  doctorId: number;
  name: string;
  email: string;
  phone: string;
  dateCreated: Date;
}

export interface CreateDoctorRequest {
  name: string;
  email: string;
  password: string;
  phone: string;
}

export interface UpdateDoctorRequest {
  name: string;
  phone: string;
  specialtyIds: number[];
}
