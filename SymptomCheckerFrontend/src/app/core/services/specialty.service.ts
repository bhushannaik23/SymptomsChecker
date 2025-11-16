import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Specialty } from '../../models/specialty.model';

@Injectable({
  providedIn: 'root'
})
export class SpecialtyService {
  private apiUrl = 'http://localhost:5202/api/specialties';

  constructor(private http: HttpClient) {}

  getSpecialties(): Observable<Specialty[]> {
    return this.http.get<Specialty[]>(this.apiUrl);
  }
}