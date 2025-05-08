import { Observable } from 'rxjs';
import {
  IAnalyzeRiskInputDto,
  IAnalyzeRiskResultDto,
  ICryptoListItemDto,
} from '../../../infrastructure/dtos/crypto.dto';
import { IPaginatedResponse } from '../../../shared/types/shared.types';

export abstract class CryptoRepository {
  abstract getAll(
    pageSize: number,
    pageIndex: number,
    searchValue?: string
  ): Observable<IPaginatedResponse<ICryptoListItemDto[]>>;

  abstract anlyzeRisk(
    cryptoId: string,
    input: IAnalyzeRiskInputDto
  ): Observable<IAnalyzeRiskResultDto>;
}
