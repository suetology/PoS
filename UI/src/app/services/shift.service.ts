import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateShiftRequest, Shift } from '../types';
import { map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShiftService {
  constructor(private http: HttpClient) { }

  createShift(request: CreateShiftRequest): Observable<Shift[]> {
    return this.http.post<Shift[]>(`${environment.API_URL}/shifts`, request).pipe(
      map((shift) => {
        return shift;
      })
    );
  }

  deleteShift(id: string) {
    return this.http.delete(`${environment.API_URL}/shifts/${id}`);
  }
}