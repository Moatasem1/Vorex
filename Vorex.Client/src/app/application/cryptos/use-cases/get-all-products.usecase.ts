import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { UseCase } from '../../abstraction/use-case';
import { ProductListItem } from '../models/product.model';
import { mapProductListItemDtoToModel } from '../mappers/product.mapper';
import { ProductRepository } from '../repositories/product.respository';

@Injectable({
  providedIn: 'root',
})
export class GetAllProductsUseCase implements UseCase<void, ProductListItem[]> {
  private _productRepository = inject(ProductRepository);

  execute(): Observable<ProductListItem[]> {
    return this._productRepository
      .getAll()
      .pipe(
        map((dtos) => dtos.map((dto) => mapProductListItemDtoToModel(dto)))
      );
  }
}
