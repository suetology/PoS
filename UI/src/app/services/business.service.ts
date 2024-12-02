import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Business, BusinessResponse } from '../types';
import { map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  constructor(private http: HttpClient) { }

  getBusiness(): Observable<Business[]>{
    return this.http.get<BusinessResponse>(`${environment.API_URL}/business`).pipe(
        map(response => response.business)
      );
  }
}
