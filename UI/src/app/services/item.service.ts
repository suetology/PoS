import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { CreateItemRequest, CreateItemVariationRequest, GetAllItemsResponse, GetAllItemVariationsResponse, Item, ItemVariation } from '../types';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  private itemsUpdated = new BehaviorSubject<void>(undefined);
  
  constructor(private http : HttpClient) { }

  getItems(): Observable<Item[]> {
    return this.http.get<GetAllItemsResponse>(`${environment.API_URL}/inventory/item`).pipe(
      map(response => response.items)
    );
  }
  
  getItem(id: string): Observable<Item>{
    return this.http.get<{item: Item}>(`${environment.API_URL}/inventory/item/${id}`).pipe(
      map((response) => response.item)
    );
  }

  getItemsUpdated(): Observable<void> {
    return this.itemsUpdated.asObservable();
  }

  createItem(request: CreateItemRequest): Observable<Item[]> {
    return this.http.post<Item[]>(`${environment.API_URL}/inventory/item`, request).pipe(
      map((item) => {
        this.itemsUpdated.next();
        return item;
      })
    );
  }

  getItemVariation(itemId: string, variationId: string): Observable<ItemVariation> {
    return this.http.get<{itemVariation: ItemVariation}>(`${environment.API_URL}/inventory/item/${itemId}/variations/${variationId}`).pipe(
      map((response) => response.itemVariation)
    );
  }

  getAllItemVariations(itemId: string): Observable<ItemVariation[]> {
    return this.http.get<GetAllItemVariationsResponse>(`${environment.API_URL}/inventory/item/${itemId}/variations`).pipe(
      map((response) => response.itemVariations)
    );
  }

  createItemVariation(itemId: string, request: CreateItemVariationRequest): Observable<ItemVariation[]> {
    return this.http.post<ItemVariation[]>(`${environment.API_URL}/inventory/item/${itemId}/variations`, request).pipe(
      map((itemVariation) => {
        return itemVariation;
      })
    )
  }
}
