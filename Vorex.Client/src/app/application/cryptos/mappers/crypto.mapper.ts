import { ICryptoListItemDto } from '../../../infrastructure/dtos/crypto.dto';
import { ICryptoListItem } from '../models/crypto.model';

export function mapCryptoListItemDtoToModel(
  crypto: ICryptoListItemDto
): ICryptoListItem {
  return {
    id: crypto.id,
    name: crypto.name,
    symbol: crypto.symbol,
  };
}
