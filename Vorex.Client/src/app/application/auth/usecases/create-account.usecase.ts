import { inject, Injectable } from '@angular/core';
import { AuthRepository } from '../repositories/auth.repository';
import { ICreateAccountInputDto } from '../../../infrastructure/dtos/auth.dto';
import { Observable } from 'rxjs';
import { UseCase } from '../../abstraction/use-case';

@Injectable({
  providedIn: 'root',
})
export class CreateAccountUseCase
  implements UseCase<ICreateAccountInputDto, boolean>
{
  private _authRepository = inject(AuthRepository);
  constructor() {}

  execute(input: ICreateAccountInputDto): Observable<boolean> {
    return this._authRepository.createAccount(input);
  }
}
