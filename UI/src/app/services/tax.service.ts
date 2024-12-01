import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Tax, TaxResponse } from '../types';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaxService {

  constructor(private httpClient : HttpClient) { }

  getTaxes(): Observable<Tax[]> {
    return this.httpClient.get<TaxResponse>(`${environment.API_URL}/tax`).pipe(
      map(response => response.taxes)
    );
  }
  
  getTax(id: string): Observable<Tax>{
    return this.httpClient.get<Tax>(`${environment.API_URL}/tax/${id}`)
  }
}
