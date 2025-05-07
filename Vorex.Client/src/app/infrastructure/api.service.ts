import { inject, Injectable } from '@angular/core';
import { environment } from '../../enviroments/enviroment';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

interface IResponse<T> {
  responseData: T;
  apiVersion: number;
}

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private _baseUrl: string = environment.url;
  private _httpClient = inject(HttpClient);

  constructor() {}

  get<O>(url: string): Observable<O> {
    return this._httpClient
      .get<IResponse<O>>(`${this._baseUrl}/${url}`)
      .pipe(map((resp) => resp.responseData));
  }
  post<I, O>(url: string, body: I): Observable<O> {
    return this._httpClient
      .post<IResponse<O>>(`${this._baseUrl}/${url}`, body)
      .pipe(map((resp) => resp.responseData));
  }

  put<I, O>(url: string, body: I): Observable<O> {
    return this._httpClient
      .put<IResponse<O>>(`${this._baseUrl}/${url}`, body)
      .pipe(map((resp) => resp.responseData));
  }
  delete<O>(url: string): Observable<O> {
    return this._httpClient
      .delete<IResponse<O>>(`${this._baseUrl}/${url}`)
      .pipe(map((resp) => resp.responseData));
  }
}
