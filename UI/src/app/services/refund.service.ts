import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { CreateRefundRequest } from "../types";

@Injectable({
  providedIn: 'root'
})
export class RefundService {
 
  constructor(private http: HttpClient) { }

    createRefund(request: CreateRefundRequest) {
        return this.http.post(`${environment.API_URL}/refunds`, request);
    }
}