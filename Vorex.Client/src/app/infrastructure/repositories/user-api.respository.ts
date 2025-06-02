import { inject, Injectable } from '@angular/core';
import { CryptoRepository } from '../../application/cryptos/repositories/crypto-repository';
import { map, Observable } from 'rxjs';
import {
  IAnalyzeRiskInputDto,
  IAnalyzeRiskResultDto,
  ICryptoListItemDto,
  IHistoricalPriceItemDto,
} from '../dtos/crypto.dto';
import { ApiService } from '../api.service';
import { IPaginatedResponse } from '../../shared/types/shared.types';
import { UserRepository } from '../../application/users/repositories/user-repository';
import {
  IAddCryptoAnlysisToCompareInputDto,
  IAddCryptoToFavoriteInputDto,
  ICryptoAnlysisCompareItemDto,
  ICryptoAnylysisHistoryDto,
  IDeleteCryptoAnlysisHistoryRecordsInputDto,
  IFavouriteCryptoDto,
} from '../dtos/user.dto';
import {
  IAddCryptoAnlysisToCompareInput,
  IFavouriteCrypto,
} from '../../application/users/models/user.model';
import { mapCryptoFavouriteDtoToModel } from '../../application/users/mappers/user.mapper';

@Injectable({
  providedIn: 'root',
})
export class UserApiRepository implements UserRepository {
  private _apiService = inject(ApiService);

  getAllCryptoAnlysisHistory(
    pageSize: number,
    pageIndex: number,
    searchValue?: string,
    startDate?: string,
    endDate?: string
  ): Observable<IPaginatedResponse<ICryptoAnylysisHistoryDto[]>> {
    const startDateParam = startDate ? `&StartDate=${startDate}` : '';
    const endDateParam = endDate ? `&EndDate=${endDate}` : '';
    const searchValueParam = searchValue ? `&SearchValue=${searchValue}` : '';

    return this._apiService.get<
      IPaginatedResponse<ICryptoAnylysisHistoryDto[]>
    >(
      `CryptoAnalysisHistory?PageSize=${pageSize}&PageIndex=${pageIndex}${startDateParam}${endDateParam}${searchValueParam}`
    );
  }

  deleteCryptoAnlysisHistoryRecords(
    input: IDeleteCryptoAnlysisHistoryRecordsInputDto
  ): Observable<boolean> {
    return this._apiService.delete<
      IDeleteCryptoAnlysisHistoryRecordsInputDto,
      boolean
    >(`CryptoAnalysisHistory`, input);
  }

  addCryptoAnlysisToCompare(
    input: IAddCryptoAnlysisToCompareInputDto
  ): Observable<boolean> {
    return this._apiService.post<IAddCryptoAnlysisToCompareInputDto, boolean>(
      `CryptoCompare`,
      input
    );
  }

  getAllCryptoAnlysisInCompare(): Observable<ICryptoAnlysisCompareItemDto[]> {
    return this._apiService.get<ICryptoAnlysisCompareItemDto[]>(
      `CryptoCompare`
    );
  }
  removeCyptoAnlysisFromCompare(
    cryptoAnlysisHistoryId: string
  ): Observable<boolean> {
    return this._apiService.delete<boolean>(
      `CryptoCompare/${cryptoAnlysisHistoryId}`
    );
  }

  // favourite
  getFavouriteCryptos(): Observable<IFavouriteCryptoDto[]> {
    return this._apiService.get<IFavouriteCryptoDto[]>(`CryptoFavourite`);
  }
  addCryptoToFavourite(
    input: IAddCryptoToFavoriteInputDto
  ): Observable<boolean> {
    return this._apiService.post<IAddCryptoToFavoriteInputDto, boolean>(
      `CryptoFavourite`,
      input
    );
  }
  removeCryptoFromFavourite(cryptoId: string): Observable<boolean> {
    return this._apiService.delete<boolean>(`CryptoFavourite/${cryptoId}`);
  }
}
