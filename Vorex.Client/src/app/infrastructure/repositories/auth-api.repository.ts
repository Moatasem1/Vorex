import { inject, Injectable } from '@angular/core';
import { CryptoRepository } from '../../application/cryptos/repositories/crypto-repository';
import { Observable } from 'rxjs';
import {
  IAnalyzeRiskInputDto,
  IAnalyzeRiskResultDto,
  ICryptoListItemDto,
  IHistoricalPriceDto,
} from '../dtos/crypto.dto';
import { ApiService } from '../api.service';
import { IPaginatedResponse } from '../../shared/types/shared.types';
import {
  ICreateAccountInputDto,
  ILoginInputDto,
  ILoginResponseDto,
  IVerfiyEmailInputDto,
} from '../dtos/auth.dto';
import { AuthRepository } from '../../application/auth/repositories/auth.repository';
import {
  mapCreateAccountInputModelToDto,
  mapVerifyEmailInputModelToDto,
} from '../../application/auth/mappers/auth.mapper';

@Injectable({
  providedIn: 'root',
})
export class AuthApiRepository implements AuthRepository {
  private _apiService = inject(ApiService);
  createAccount(input: ICreateAccountInputDto): Observable<boolean> {
    return this._apiService.post<ICreateAccountInputDto, boolean>(
      `Auth/register`,
      mapCreateAccountInputModelToDto(input)
    );
  }

  verifyAccountEmail(input: IVerfiyEmailInputDto): Observable<boolean> {
    return this._apiService.post<IVerfiyEmailInputDto, boolean>(
      `Auth/verify-email`,
      input
    );
  }

  login(input: ILoginInputDto): Observable<ILoginResponseDto> {
    return this._apiService.post<ILoginInputDto, ILoginResponseDto>(
      `Auth/login`,
      input
    );
  }
}
