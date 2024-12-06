import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Discount, DiscountRequest } from '../types';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DiscountService {

  constructor(private http: HttpClient) { }

    getDiscounts(): Observable<Discount[]> {
        return this.http.get<Discount[]>(`${environment.API_URL}/discount`);
    }
    
    addDiscount(discountRequest: Partial<DiscountRequest>): Observable<Discount[]> {
    return this.http.post<Discount[]>(`${environment.API_URL}/discount`, discountRequest);
    }
}