import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Tax, TaxRequest, TaxResponse, UpdateTaxRequest } from '../types';
import { BehaviorSubject, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaxService {

  private taxesUpdated = new BehaviorSubject<void>(undefined);
  
  constructor(private http : HttpClient) { }

  getTaxes(): Observable<Tax[]> {
    return this.http.get<TaxResponse>(`${environment.API_URL}/tax/valid`).pipe(
      map(response => response.taxes)
    );
  }
  
  getTax(id: string): Observable<Tax>{
    return this.http.get<{tax: Tax}>(`${environment.API_URL}/tax/${id}`).pipe(
      map((response) => response.tax)
    );
  }

  getTaxesUpdated(): Observable<void> {
    return this.taxesUpdated.asObservable();
  }

  addTax(taxRequest: TaxRequest): Observable<Tax[]> {
    return this.http.post<Tax[]>(`${environment.API_URL}/tax`, taxRequest).pipe(
      map((tax) => {
        this.taxesUpdated.next();
        return tax;
      })
    );
  }

  updateTax(id: string, request: UpdateTaxRequest): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/tax/${id}`, request).pipe(
      map(() => {
        this.taxesUpdated.next();
      })
    );
  }
}
