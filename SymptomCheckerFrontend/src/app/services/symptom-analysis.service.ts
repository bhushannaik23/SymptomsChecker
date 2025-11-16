import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import { SymptomAnalysisRequest, SymptomAnalysisResponse } from '../models/symptom-analysis.models';

@Injectable({
  providedIn: 'root'
})
export class SymptomAnalysisService {
  constructor(private apiService: ApiService) {}

  analyzeSymptoms(request: SymptomAnalysisRequest): Observable<SymptomAnalysisResponse> {
    return this.apiService.post<SymptomAnalysisResponse>('symptomanalysis/analyze', request);
  }
}