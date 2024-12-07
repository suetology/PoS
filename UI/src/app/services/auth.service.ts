import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, map, Observable, of } from 'rxjs';
import { LoginRequest, LoginResponse, LogoutRequest, RefreshAccessTokenResponse } from '../types';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(this.getInitialLoggedIn());
  private username = new BehaviorSubject<string>(this.getInitialUsername() ?? '');
  
  isLoggedIn$ = this.loggedIn.asObservable();
  username$ = this.username.asObservable();

  constructor(private http: HttpClient) { }

  private getInitialLoggedIn(): boolean {
    return !!this.getAccessToken() && !!this.getInitialUsername();
  }
  
  private getInitialUsername(): string | null {
    return localStorage.getItem('username');
  }

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
            localStorage.setItem('username', username);

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
    localStorage.removeItem('username');

    if (!refreshToken) {
      return of(false);
    }

    const payload: LogoutRequest = { refreshToken };

    return this.http.post(`${environment.API_URL}/auth/logout`, payload)
      .pipe(
        catchError(error => {
          console.log(error);

          return of(false);
        })
      );
  }

  refreshAccessToken(): Observable<string> {
    const refreshToken = this.getRefreshToken();

    if (!refreshToken) {
      throw new Error("No refresh token available");
    }
    
    const payload: LogoutRequest = { refreshToken };

    return this.http.post<RefreshAccessTokenResponse>(`${environment.API_URL}/auth/refresh-token`, payload)
      .pipe(
        map(response => {
          if (response && response.accessToken && response.refreshToken) {
            this.loggedIn.next(true);

            localStorage.setItem('accessToken', response.accessToken);
            localStorage.setItem('refreshToken', response.refreshToken);

            return response.accessToken;
          }

          return '';
        }),
        catchError(error => {
          console.log(error);
          this.logout();
          return of('');
        })
      );
  }
}
