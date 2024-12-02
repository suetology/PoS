import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Discount, DiscountRequest } from '../types';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DiscountService {

  constructor(private http: HttpClient) { }

    getDiscounts(): Observable<Discount[]> {
        return this.http.get<Discount[]>(`${environment.API_URL}/discount`);
    }
    
    // addDiscount(discountRequest: DiscountRequest): Observable<Discount[]> {
    // return this.http.post<Discount[]>(`${environment.API_URL}/discount`, discountRequest);
    // }

    // private discountData$ = new BehaviorSubject<DiscountRequest>({
    //     discountName: "",
    //     value: 0,
    //     isPercentage: false,
    //     amountAvailable: 0,
    //     validFrom: new Date,
    //     validTo: new Date
    //   });

    //   setDiscountData(formData: DiscountRequest) {
    //     this.discountData$.next(formData);
    //   }
}