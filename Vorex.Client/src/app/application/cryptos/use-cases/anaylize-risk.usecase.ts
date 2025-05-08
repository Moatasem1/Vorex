import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import { IAnalyzeRiskInput, IAnalyzeRiskResult } from '../models/crypto.model';
import { CryptoRepository } from '../repositories/crypto-repository';
import { map, Observable } from 'rxjs';
import {
  mapCryptoAnalizeRiskInputToDto,
  mapCryptoAnalizeRiskResultDtoToModel,
} from '../mappers/crypto.mapper';

@Injectable({ providedIn: 'root' })
export class AnaylizeRiskUseCase
  implements UseCase<IAnalyzeRiskInput, IAnalyzeRiskResult>
{
  private _cryptoRepository = inject(CryptoRepository);

  execute(input: IAnalyzeRiskInput): Observable<IAnalyzeRiskResult> {
    return this._cryptoRepository
      .anlyzeRisk(input.cryptoId, mapCryptoAnalizeRiskInputToDto(input))
      .pipe(map(mapCryptoAnalizeRiskResultDtoToModel));
  }
}
