import { Observable } from 'rxjs';
import { ProductListItemDto } from '../../../infrastructure/dtos/product.dto';

export abstract class ProductRepository {
  abstract getAll(): Observable<ProductListItemDto[]>;
}
