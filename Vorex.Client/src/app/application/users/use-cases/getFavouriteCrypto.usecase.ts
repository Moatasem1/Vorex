import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import { IFavouriteCrypto } from '../models/user.model';
import { map, Observable } from 'rxjs';
import { UserRepository } from '../repositories/user-repository';
import { mapCryptoFavouriteDtoToModel } from '../mappers/user.mapper';

@Injectable({
  providedIn: 'root',
})
export class GetFavouriteCryptosUseCase
  implements UseCase<void, IFavouriteCrypto[]>
{
  private _userRepository = inject(UserRepository);
  execute(input: void): Observable<IFavouriteCrypto[]> {
    return this._userRepository
      .getFavouriteCryptos()
      .pipe(
        map((resp) => resp.map((item) => mapCryptoFavouriteDtoToModel(item)))
      );
  }
}
