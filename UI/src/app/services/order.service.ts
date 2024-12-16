import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { AddItemInOrderRequest, AddTipRequest, CreateOrderRequest, GetAllOrdersResponse, Order, UpdateOrderRequest } from '../types';

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

  updateOrder(id: string, request: UpdateOrderRequest): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/orders/${id}/quantity`, request).pipe(
      map(() => {
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

  addItemToOrder(orderId: string, request: AddItemInOrderRequest): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/orders/${orderId}/add-item`, request).pipe(
      tap(() => this.ordersUpdated.next())
    );
  }
}
