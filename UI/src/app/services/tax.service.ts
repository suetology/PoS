import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Tax, TaxRequest, TaxResponse } from '../types';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaxService {

  constructor(private http : HttpClient) { }

  getTaxes(): Observable<Tax[]> {
    return this.http.get<TaxResponse>(`${environment.API_URL}/tax`).pipe(
      map(response => response.taxes)
    );
  }
  
  getTax(id: string): Observable<Tax>{
    return this.http.get<{tax: Tax}>(`${environment.API_URL}/tax/${id}`).pipe(
      map((response) => response.tax)
    );
  }

  addTax(taxRequest: TaxRequest): Observable<Tax[]> {
    return this.http.post<Tax[]>(`${environment.API_URL}/tax`, taxRequest);
    }
}
