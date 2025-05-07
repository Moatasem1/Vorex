import { ProductListItemDto } from '../../../infrastructure/dtos/product.dto';
import { ProductListItem } from '../models/product.model';

export function mapProductListItemDtoToModel(
  product: ProductListItemDto
): ProductListItem {
  return {
    id: product.id,
    name: product.title,
    price: product.price,
    description: product.description,
  };
}
