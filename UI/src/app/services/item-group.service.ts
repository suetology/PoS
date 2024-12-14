import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { CreateItemGroupRequest, GetAllItemGroupsResponse, ItemGroup, UpdateItemGroupRequest } from '../types';

@Injectable({
  providedIn: 'root'
})
export class ItemGroupService {

  private itemGroupsUpdated = new BehaviorSubject<void>(undefined);
  
  constructor(private http: HttpClient) { }

  getAllItemGroups(): Observable<ItemGroup[]> {
    return this.http.get<GetAllItemGroupsResponse>(`${environment.API_URL}/inventory/item-Group`).pipe(
      map(response => response.itemGroups)
    );
  }
  
  getItemGroup(id: string): Observable<ItemGroup>{
    return this.http.get<{itemGroup: ItemGroup}>(`${environment.API_URL}/inventory/item-Group/${id}`).pipe(
      map((response) => response.itemGroup)
    );
  }

  getItemGroupsUpdated(): Observable<void> {
    return this.itemGroupsUpdated.asObservable();
  }

  createItemGroup(request: CreateItemGroupRequest): Observable<ItemGroup[]> {
    return this.http.post<ItemGroup[]>(`${environment.API_URL}/inventory/item-Group`, request).pipe(
      map((itemGroup) => {
        this.itemGroupsUpdated.next();
        return itemGroup;
      })
    );
  }

  updateItemGroup(id: string, request: UpdateItemGroupRequest): Observable<void> {
    return this.http.patch<void>(`${environment.API_URL}/inventory/item-Group/${id}`, request).pipe(
      map(() => {
        this.itemGroupsUpdated.next();
      })
    );
  }
}
