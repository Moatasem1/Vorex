import { Injectable } from '@angular/core';
import {
  ICreateAccountInputDto,
  ILoginInputDto,
  ILoginResponseDto,
  IVerfiyEmailInputDto as IVerfiyAccountEmailInputDto,
} from '../../../infrastructure/dtos/auth.dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export abstract class AuthRepository {
  abstract createAccount(input: ICreateAccountInputDto): Observable<boolean>;
  abstract verifyAccountEmail(
    input: IVerfiyAccountEmailInputDto
  ): Observable<boolean>;
  abstract login(input: ILoginInputDto): Observable<ILoginResponseDto>;
}
