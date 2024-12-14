import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { AddTipRequest, CreateOrderRequest, GetAllOrdersResponse, Order } from '../types';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private ordersUpdated = new BehaviorSubject<void>(undefined);
  
  constructor(private http : HttpClient) { }

  getOrders(): Observable<Order[]> {
    return this.http.get<GetAllOrdersResponse>(`${environment.API_URL}/orders`).pipe(
      map(response => response.orders)
    );
  }
  
  getOrder(id: string): Observable<Order>{
    return this.http.get<{order: Order}>(`${environment.API_URL}/orders/${id}`).pipe(
      map((response) => response.order)
    );
  }

  getOrdersUpdated(): Observable<void> {
    return this.ordersUpdated.asObservable();
  }

  createOrder(request: CreateOrderRequest): Observable<Order[]> {
    return this.http.post<Order[]>(`${environment.API_URL}/orders`, request).pipe(
      map((order) => {
        this.ordersUpdated.next();
        return order;
      })
    );
  }

  cancelOrder(id: string) {
    return this.http.post(`${environment.API_URL}/orders/${id}/cancel`, {}).pipe(
      tap(() => {
        this.ordersUpdated.next();
      })
    );
  }

  addTip(id: string, request: AddTipRequest) {
    return this.http.post(`${environment.API_URL}/orders/${id}/tip`, request).pipe(
      tap(() => {
        this.ordersUpdated.next();
      })
    );
  }
}
