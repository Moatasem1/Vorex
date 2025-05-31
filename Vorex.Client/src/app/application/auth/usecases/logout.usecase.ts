import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import {
  ILoginInput,
  ILoginResponse,
  ILogoutInput,
} from '../models/auth.model';
import { AuthRepository } from '../repositories/auth.repository';
import { map, Observable } from 'rxjs';
import {
  mapLoginInputModelToDto,
  mapLoginResponseDtoToModel,
  mapLogoutInputModelToDto,
} from '../mappers/auth.mapper';

@Injectable({
  providedIn: 'root',
})
export class LogOutUseCase implements UseCase<ILogoutInput, boolean> {
  private _authRepository = inject(AuthRepository);
  constructor() {}

  execute(input: ILogoutInput): Observable<boolean> {
    return this._authRepository.logout(mapLogoutInputModelToDto(input));
  }
}
