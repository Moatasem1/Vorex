import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import { Observable } from 'rxjs';
import { UserRepository } from '../repositories/user-repository';

@Injectable({
  providedIn: 'root',
})
export class removeCryptoFromFavouriteUseCase
  implements UseCase<string, boolean>
{
  private _userRepository = inject(UserRepository);
  execute(input: string): Observable<boolean> {
    return this._userRepository.removeCryptoFromFavourite(input);
  }
}
