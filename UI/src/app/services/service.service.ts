import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AvailableTime, CreateServiceRequest, GetAllServicesResponse, GetAvailableTimesResponse, Service, UpdateServiceRequest } from '../types';

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

  updateService(serviceId: string, request: UpdateServiceRequest): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/services/${serviceId}`, request).pipe(
      map(() => {
        this.servicesUpdated.next();
      })
    );
  }

  getAvailableTimes(serviceId: string, date: string): Observable<AvailableTime[]> {
    return this.http.get<GetAvailableTimesResponse>(`${environment.API_URL}/services/${serviceId}/available-times?date=${date}`).pipe(
      map(response => {
        return response.availableTimes
      })
    );
  }

  retireService(id: string): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/services/${id}/retire`, {}).pipe(
      map(() => {
        this.servicesUpdated.next();
      })
    );
  }
}