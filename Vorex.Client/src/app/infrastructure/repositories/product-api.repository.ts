import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ProductListItemDto } from '../../infrastructure/dtos/product.dto';
import { ProductRepository } from '../../application/cryptos/repositories/product.respository';

@Injectable({
  providedIn: 'root',
})
export class ProductApiRepository implements ProductRepository {
  private _httpClient = inject(HttpClient);
  getAll(): Observable<ProductListItemDto[]> {
    return this._httpClient.get<ProductListItemDto[]>(
      'https://fakestoreapi.com/products'
    );
  }
}
