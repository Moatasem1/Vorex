import { Injectable } from '@angular/core';
import {
  ICreateAccountInputDto,
  ILoginInputDto,
  ILoginResponseDto,
  ILogoutInputDto,
  IRefreshTokenInputDto,
  IRefreshTokenResponseDto,
  IVerfiyEmailInputDto as IVerfiyAccountEmailInputDto,
} from '../../../infrastructure/dtos/auth.dto';
import { Observable } from 'rxjs';
import { IRefreshTokenInput } from '../models/auth.model';

@Injectable({
  providedIn: 'root',
})
export abstract class AuthRepository {
  abstract createAccount(input: ICreateAccountInputDto): Observable<boolean>;
  abstract verifyAccountEmail(
    input: IVerfiyAccountEmailInputDto
  ): Observable<boolean>;
  abstract login(input: ILoginInputDto): Observable<ILoginResponseDto>;
  abstract refreshToken(
    input: IRefreshTokenInputDto
  ): Observable<IRefreshTokenResponseDto>;

  abstract logout(input: ILogoutInputDto): Observable<boolean>;
}
