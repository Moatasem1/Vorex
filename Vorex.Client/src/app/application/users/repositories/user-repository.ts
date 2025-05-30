import { Observable } from 'rxjs';
import { ICryptoListItemDto } from '../../../infrastructure/dtos/crypto.dto';
import { IPaginatedResponse } from '../../../shared/types/shared.types';
import {
  IAddCryptoAnlysisToCompareInputDto,
  IAddCryptoToFavoriteInputDto,
  ICryptoAnlysisCompareItemDto,
  ICryptoAnylysisHistoryDto,
  IDeleteCryptoAnlysisHistoryRecordsInputDto,
  IFavouriteCryptoDto,
} from '../../../infrastructure/dtos/user.dto';
import {
  IAddCryptoAnlysisToCompareInput,
  IFavouriteCrypto,
} from '../models/user.model';

export abstract class UserRepository {
  abstract getAllCryptoAnlysisHistory(
    pageSize: number,
    pageIndex: number,
    searchValue?: string,
    startDate?: string,
    endDate?: string
  ): Observable<IPaginatedResponse<ICryptoAnylysisHistoryDto[]>>;

  abstract deleteCryptoAnlysisHistoryRecords(
    input: IDeleteCryptoAnlysisHistoryRecordsInputDto
  ): Observable<boolean>;

  abstract addCryptoAnlysisToCompare(
    input: IAddCryptoAnlysisToCompareInputDto
  ): Observable<boolean>;

  abstract getAllCryptoAnlysisInCompare(): Observable<
    ICryptoAnlysisCompareItemDto[]
  >;

  abstract removeCyptoAnlysisFromCompare(
    cryptoAnlysisHistoryId: string
  ): Observable<boolean>;

  abstract getFavouriteCryptos(): Observable<IFavouriteCryptoDto[]>;
  abstract addCryptoToFavourite(
    input: IAddCryptoToFavoriteInputDto
  ): Observable<boolean>;
  abstract removeCryptoFromFavourite(cryptoId: string): Observable<boolean>;
}
