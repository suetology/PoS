import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Business, BusinessResponse, UpdateBusinessRequest } from '../types';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  constructor(private http: HttpClient) { }

  private businessUpdated = new BehaviorSubject<void>(undefined);
  
  getBusiness(): Observable<Business[]>{
    return this.http.get<BusinessResponse>(`${environment.API_URL}/business`).pipe(
        map(response => response.businesses)
      );
  }

  getBusinessById(id: string): Observable<Business>{
    return this.http.get<{business: Business}>(`${environment.API_URL}/business/${id}`).pipe(
      map((response) => response.business)
    );
  }

  updateBusiness(businessId: string, request: UpdateBusinessRequest): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/business/${businessId}`, request).pipe(
      map(() => {
        this.businessUpdated.next();
      })
    );
  }

  getBusinessUpdated(): Observable<void> {
    return this.businessUpdated.asObservable();
  }
}
