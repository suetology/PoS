import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { CreateCustomerRequest, Customer, GetAllCustomersResponse, UpdateCustomerRequest } from '../types';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private customersUpdated = new BehaviorSubject<void>(undefined);
  
  constructor(private http: HttpClient) { }

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<GetAllCustomersResponse>(`${environment.API_URL}/customer`).pipe(
      map(response => response.customers)
    );
  }
  
  getCustomer(id: string): Observable<Customer>{
    return this.http.get<{customer: Customer}>(`${environment.API_URL}/customer/${id}`).pipe(
      map((response) => response.customer)
    );
  }

  getCustomersUpdated(): Observable<void> {
    return this.customersUpdated.asObservable();
  }

  createCustomer(request: CreateCustomerRequest): Observable<Customer[]> {
    return this.http.post<Customer[]>(`${environment.API_URL}/customer`, request).pipe(
      map((customer) => {
        this.customersUpdated.next();
        return customer;
      })
    );
  }

  updateCustomer(id: string, request: UpdateCustomerRequest): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/customer/${id}`, request).pipe(
      map(() => {
        this.customersUpdated.next();
      })
    );
  }
}