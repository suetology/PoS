import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CreateServiceRequest, GetAllServicesResponse, Service } from '../types';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  private servicesUpdated = new BehaviorSubject<void>(undefined);
  
  constructor(private http: HttpClient) { }
  
  getServices(): Observable<Service[]> {
    return this.http.get<GetAllServicesResponse>(`${environment.API_URL}/services`).pipe(
      map(response => response.services)
    );
  }
  
  getService(id: string): Observable<Service>{
    return this.http.get<{service: Service}>(`${environment.API_URL}/services/${id}`).pipe(
      map((response) => response.service)
    );
  }

  getServicesUpdated(): Observable<void> {
    return this.servicesUpdated.asObservable();
  }

  createService(request: CreateServiceRequest): Observable<Service[]> {
    return this.http.post<Service[]>(`${environment.API_URL}/services`, request).pipe(
      map((service) => {
        this.servicesUpdated.next();
        return service;
      })
    );
  }
}