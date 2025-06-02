import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import {
  IAnalyzeRiskInput,
  IAnalyzeRiskResult,
  ICryptoHistoricalPrice,
  ICryptoHistoricalPriceItem,
  IGetCryptoHistoricalPricesInput,
} from '../models/crypto.model';
import { CryptoRepository } from '../repositories/crypto-repository';
import { map, Observable } from 'rxjs';
import {
  mapCryptoAnalizeRiskInputToDto,
  mapCryptoAnalizeRiskResultDtoToModel,
  mapCryptoHistoricalPriceDtoToModel,
} from '../mappers/crypto.mapper';

@Injectable({ providedIn: 'root' })
export class GetCryptoHistoricalPricesUseCase
  implements UseCase<IGetCryptoHistoricalPricesInput, ICryptoHistoricalPrice>
{
  private _cryptoRepository = inject(CryptoRepository);

  execute(
    input: IGetCryptoHistoricalPricesInput
  ): Observable<ICryptoHistoricalPrice> {
    return this._cryptoRepository
      .getHistoricalPrices(input.cryptoId, input.startDate, input.endDate)
      .pipe(map((dto) => mapCryptoHistoricalPriceDtoToModel(dto)));
  }
}
