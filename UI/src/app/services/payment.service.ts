import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { environment } from "../../environments/environment";
import { CreateCardPaymentRequest, CreateCardPaymentResponse, CreateCashOrGiftCardPaymentRequest } from "../types";

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
 
  constructor(private http: HttpClient) { }

	createCashOrGiftCardPayment(request: CreateCashOrGiftCardPaymentRequest) {
		return this.http.post(`${environment.API_URL}/payments/cash-giftcard`, request);
	}

	createCardPayment(request: CreateCardPaymentRequest) {
		return this.http.post<CreateCardPaymentResponse>(`${environment.API_URL}/payments/card`, request).pipe(
			map(response => response.sessionUrl)
		);
	}
}