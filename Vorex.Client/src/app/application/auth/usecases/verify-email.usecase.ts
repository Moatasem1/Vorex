import { inject, Injectable } from '@angular/core';
import { AuthRepository } from '../repositories/auth.repository';
import { ICreateAccountInputDto } from '../../../infrastructure/dtos/auth.dto';
import { Observable } from 'rxjs';
import { UseCase } from '../../abstraction/use-case';
import { IVerfiyAccountEmailInput } from '../models/auth.model';
import { mapVerifyEmailInputModelToDto } from '../mappers/auth.mapper';

@Injectable({
  providedIn: 'root',
})
export class VerifyEmailUseCase
  implements UseCase<IVerfiyAccountEmailInput, boolean>
{
  private _authRepository = inject(AuthRepository);
  constructor() {}

  execute(input: IVerfiyAccountEmailInput): Observable<boolean> {
    return this._authRepository.verifyAccountEmail(
      mapVerifyEmailInputModelToDto(input)
    );
  }
}
