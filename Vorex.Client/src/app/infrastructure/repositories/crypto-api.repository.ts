import { inject, Injectable } from '@angular/core';
import { CryptoRepository } from '../../application/cryptos/repositories/crypto-repository';
import { Observable } from 'rxjs';
import {
  IAnalyzeRiskInputDto,
  IAnalyzeRiskResultDto,
  ICryptoListItemDto,
} from '../dtos/crypto.dto';
import { ApiService } from '../api.service';
import { IPaginatedResponse } from '../../shared/types/shared.types';

@Injectable({
  providedIn: 'root',
})
export class CryptoApiRepository implements CryptoRepository {
  private _apiService = inject(ApiService);
  getAll(
    pageSize: number,
    pageIndex: number,
    searchValue?: string
  ): Observable<IPaginatedResponse<ICryptoListItemDto[]>> {
    return this._apiService.get<IPaginatedResponse<ICryptoListItemDto[]>>(
      `Crypto?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchValue=${searchValue}`
    );
  }

  anlyzeRisk(
    cryptoId: string,
    input: IAnalyzeRiskInputDto
  ): Observable<IAnalyzeRiskResultDto> {
    return this._apiService.post<IAnalyzeRiskInputDto, IAnalyzeRiskResultDto>(
      `Crypto/${cryptoId}/analyze-risk`,
      input
    );
  }
}
