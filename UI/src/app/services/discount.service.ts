import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Discount, DiscountRequest, DiscountResponse } from '../types';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DiscountService {

  private discountUpdated = new BehaviorSubject<void>(undefined);

  constructor(private http: HttpClient) { }

    getDiscounts(): Observable<Discount[]> {
        return this.http.get<DiscountResponse>(`${environment.API_URL}/discount`).pipe(
          map(response => response.discounts)
        );
    }

    getDiscount(id: string): Observable<Discount>{
      return this.http.get<{discount: Discount}>(`${environment.API_URL}/discount/${id}`).pipe(
        map((response) => response.discount)
      );
    }

    getDiscountsUpdated(): Observable<void> {
      return this.discountUpdated.asObservable();
    }

    triggerDiscountsUpdated(): void {
      this.discountUpdated.next();
    }
    
    addDiscount(discountRequest: DiscountRequest): Observable<Discount[]> {
      return this.http.post<Discount[]>(`${environment.API_URL}/discount`, discountRequest).pipe(
        map((discounts) => {
          this.triggerDiscountsUpdated();
          return discounts;
        })
      );
    }

    deleteDiscount(id: string) {
      return this.http.delete(`${environment.API_URL}/discount/${id}`);
    }
}