import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateUserRequest } from 'src/app/core/models/auth.model';
import { Country, Province } from 'src/app/core/models/location.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  register(userData: CreateUserRequest): Observable<any> {
    return this.http.post('/api/users', userData);
  }
  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>('/api/countries');
  }

  getProvinces(countryId: number): Observable<Province[]> {
    return this.http.get<Province[]>(`/api/provinces/by-country/${countryId}`);
  }
}
