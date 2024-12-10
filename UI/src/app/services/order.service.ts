import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { CreateOrderRequest, GetAllOrdersResponse, Order } from '../types';

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
}
