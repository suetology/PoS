import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, map, Observable, of, throwError } from 'rxjs';
import { LoginRequest, LoginResponse, LogoutRequest } from '../types';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);
  private username = new BehaviorSubject<string>('');
  
  isLoggedIn$ = this.loggedIn.asObservable();
  username$ = this.username.asObservable();

  constructor(private http: HttpClient) { }

  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  login(username: string, password: string): Observable<boolean> {
    const payload: LoginRequest = { username, password };

    return this.http.post<LoginResponse>(`${environment.API_URL}/auth/login`, payload)
      .pipe(
        map(response => {
          if (response && response.accessToken && response.refreshToken) {
            this.loggedIn.next(true);
            this.username.next(username); 

            localStorage.setItem('accessToken', response.accessToken);
            localStorage.setItem('refreshToken', response.refreshToken);

            console.log(response.accessToken);
            console.log(response.refreshToken);

            return true;
          }

          return false;
      }),
      catchError(error => {
        console.log(error);

        return of(false);
      })
    );
  }

  logout(): Observable<any> {
    this.loggedIn.next(false);
    this.username.next('');

    const refreshToken = this.getRefreshToken();

    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');

    if (refreshToken == null) {
      return of(false);
    }

    const payload: LogoutRequest = { refreshToken };

    return this.http.post(`${environment.API_URL}/auth/logout`, payload)
      .pipe(
        catchError(error => {
          console.log(error);

          return of(false);
        })
      )
  }
}
