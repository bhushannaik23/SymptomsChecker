export interface SymptomAnalysisRequest {
  patientId: number;
  symptomsText: string;
}

export interface SymptomAnalysisResponse {
  predictedDisease: string;
  cause: string;
  cure: string;
  recommendedSpecialty: string;
  recommendedDoctors: RecommendedDoctor[];
}

export interface RecommendedDoctor {
  doctorId: number;
  name: string;
  email: string;
  phone: string;
}
