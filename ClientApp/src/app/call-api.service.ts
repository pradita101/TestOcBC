import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { Observable, throwError, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CallApiService {
  constructor(
    private http: HttpClient
  ) { }
  endpoint = window.location.origin;

  AddCustomer(data): Observable<any> {
    return this.http.post(`${this.endpoint}api/Customers`, data);
  }

  AddTransaction(data): Observable<any> {
    return this.http.post(`${this.endpoint}api/Transactions`, data);
  }

  ShowTransaction(CustId:number): Observable<any> {
    return this.http.get(`${this.endpoint}api/Transactions/` + CustId.toString());
  }

  ShowCustomer(): Observable<any> {
    return this.http.get(`${this.endpoint}api/Customers`);
  }
}
