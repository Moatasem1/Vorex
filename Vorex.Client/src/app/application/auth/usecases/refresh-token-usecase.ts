import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import {
  ILoginInput,
  ILoginResponse,
  IRefreshTokenInput,
  IRefreshTokenResponse,
} from '../models/auth.model';
import { AuthRepository } from '../repositories/auth.repository';
import { map, Observable } from 'rxjs';
import {
  mapLoginInputModelToDto,
  mapLoginResponseDtoToModel,
  mapRefreshTokenInputModelToDto,
  mapRefreshTokenResponseDtoToModel,
} from '../mappers/auth.mapper';

@Injectable({
  providedIn: 'root',
})
export class RefreshTokenUseCase
  implements UseCase<IRefreshTokenInput, IRefreshTokenResponse>
{
  private _authRepository = inject(AuthRepository);
  constructor() {}

  execute(input: IRefreshTokenInput): Observable<IRefreshTokenResponse> {
    return this._authRepository
      .refreshToken(mapRefreshTokenInputModelToDto(input))
      .pipe(map((resp) => mapRefreshTokenResponseDtoToModel(resp)));
  }
}
