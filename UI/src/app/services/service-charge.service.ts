import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ServiceCharge, ServiceChargeRequest, ServiceChargeResponse, UpdateServiceChargeRequest } from '../types';

@Injectable({
  providedIn: 'root'
})
export class ServiceChargeService {

  private serviceChargesUpdated = new BehaviorSubject<void>(undefined);
  
  constructor(private http : HttpClient) { }

  getServiceCharges(): Observable<ServiceCharge[]> {
    return this.http.get<ServiceChargeResponse>(`${environment.API_URL}/serviceCharge/valid`).pipe(
      map(response => response.serviceCharges)
    );;
  }

  getServiceCharge(id: string): Observable<ServiceCharge>{
    return this.http.get<{serviceCharge: ServiceCharge}>(`${environment.API_URL}/serviceCharge/${id}`).pipe(
      map((response) => response.serviceCharge)
    );
  }

  getserviceChargesUpdated(): Observable<void> {
    return this.serviceChargesUpdated.asObservable();
  }

  addServiceCharge(serviceChargeRequest: ServiceChargeRequest): Observable<ServiceCharge[]> {
    return this.http.post<ServiceCharge[]>(`${environment.API_URL}/serviceCharge`, serviceChargeRequest).pipe(
      map((serviceCharge) => {
        this.serviceChargesUpdated.next();
        return serviceCharge;
      })
    );
  }

  updateServiceCharge(id: string, request: UpdateServiceChargeRequest): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/serviceCharge/${id}`, request).pipe(
      map(() => {
        this.serviceChargesUpdated.next();
      })
    );
  }
}
