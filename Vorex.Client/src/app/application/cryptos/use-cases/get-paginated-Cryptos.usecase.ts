import { inject, Injectable } from '@angular/core';
import { ICryptoListItem } from '../models/crypto.model';
import { UseCase } from '../../abstraction/use-case';
import { CryptoRepository } from '../repositories/crypto-repository';
import { map, Observable } from 'rxjs';
import { mapCryptoListItemDtoToModel } from '../mappers/crypto.mapper';
import {
  IBasicPaginatedInput,
  IPaginatedResponse,
} from '../../../shared/types/shared.types';

@Injectable({
  providedIn: 'root',
})
export class GetPaginatedCryptosUseCase
  implements
    UseCase<IBasicPaginatedInput, IPaginatedResponse<ICryptoListItem[]>>
{
  private _cryptoRepository = inject(CryptoRepository);

  execute(
    input: IBasicPaginatedInput
  ): Observable<IPaginatedResponse<ICryptoListItem[]>> {
    return this._cryptoRepository
      .getAll(input.PageSize, input.PageIndex, input.SearchValue?.trim())
      .pipe(
        map((resp) => {
          return {
            ...resp,
            data: resp.data.map(mapCryptoListItemDtoToModel),
          };
        })
      );
  }
}
