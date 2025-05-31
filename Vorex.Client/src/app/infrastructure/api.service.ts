import { inject, Injectable } from '@angular/core';
import { environment } from '../../enviroments/enviroment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';
import { IError } from '../shared/types/shared.types';

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
    return this._httpClient.get<IResponse<O>>(`${this._baseUrl}/${url}`).pipe(
      map((resp) => resp.responseData),
      catchError(this.reShapeError)
    );
  }
  post<I, O>(url: string, body: I): Observable<O> {
    return this._httpClient
      .post<IResponse<O>>(`${this._baseUrl}/${url}`, body)
      .pipe(
        map((resp) => resp.responseData),
        catchError(this.reShapeError)
      );
  }

  put<I, O>(url: string, body: I): Observable<O> {
    return this._httpClient
      .put<IResponse<O>>(`${this._baseUrl}/${url}`, body)
      .pipe(
        map((resp) => resp.responseData),
        catchError(this.reShapeError)
      );
  }

  delete<I, O>(url: string, body: I): Observable<O>;
  delete<O>(url: string): Observable<O>;
  delete<I, O>(url: string, body?: I): Observable<O> {
    return this._httpClient
      .delete<IResponse<O>>(`${this._baseUrl}/${url}`, { body: body })
      .pipe(
        map((resp) => resp.responseData),
        catchError(this.reShapeError)
      );
  }

  //
  private reShapeError(error: HttpErrorResponse): Observable<never> {
    const errors = error.error?.responseData.errors;

    return throwError(() => errors);
  }
}
