import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import { ILoginInput, ILoginResponse } from '../models/auth.model';
import { AuthRepository } from '../repositories/auth.repository';
import { map, Observable } from 'rxjs';
import {
  mapLoginInputModelToDto,
  mapLoginResponseDtoToModel,
} from '../mappers/auth.mapper';

@Injectable({
  providedIn: 'root',
})
export class LoginUseCase implements UseCase<ILoginInput, ILoginResponse> {
  private _authRepository = inject(AuthRepository);
  constructor() {}

  execute(input: ILoginInput): Observable<ILoginResponse> {
    return this._authRepository
      .login(mapLoginInputModelToDto(input))
      .pipe(map((resp) => mapLoginResponseDtoToModel(resp)));
  }
}
