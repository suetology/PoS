import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { CreateUserRequest, GetAllUsersResponse, Role, User } from '../types';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private usersUpdated = new BehaviorSubject<void>(undefined);
  
  constructor(private http : HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<GetAllUsersResponse>(`${environment.API_URL}/users`).pipe(
      map(response => response.users.filter(u => u.role != Role.SuperAdmin))
    );
  }
  
  getUser(id: string): Observable<User>{
    return this.http.get<{user: User}>(`${environment.API_URL}/users/${id}`).pipe(
      map((response) => response.user)
    );
  }

  getUsersUpdated(): Observable<void> {
    return this.usersUpdated.asObservable();
  }

  addUser(createUserRequest: CreateUserRequest): Observable<any> {
    return this.http.post(`${environment.API_URL}/users`, createUserRequest).pipe(
      map((tax) => {
        this.usersUpdated.next();
        return tax;
      })
    );
  }
}