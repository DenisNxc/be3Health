import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Paciente } from '../interfaces/patient.interface';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private api = 'http://localhost:5010/api/paciente';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Paciente[]> {
    return this.http.get<Paciente[]>(this.api);
  }

  deactivate(id: number): Observable<any> {
    return this.http.delete(`${this.api}/${id}`);
  }

  getById(id: number): Observable<Paciente> {
    return this.http.get<Paciente>(`${this.api}/${id}`);
  }

  save(patient: Paciente): Observable<Paciente> {
    return this.http.post<Paciente>(this.api, patient);
  }

  update(patient: Paciente): Observable<Paciente> {
    return this.http.put<Paciente>(`${this.api}/${patient.id}`, patient);
  }

  getConvenios(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:5010/api/convenio');
  }
}
