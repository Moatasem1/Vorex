import { Observable } from 'rxjs';
import { ICryptoListItemDto } from '../../../infrastructure/dtos/crypto.dto';

export abstract class CryptoRepository {
  abstract getAll(
    pageSize: number,
    pageIndex: number,
    searchValue?: string
  ): Observable<ICryptoListItemDto[]>;
}
