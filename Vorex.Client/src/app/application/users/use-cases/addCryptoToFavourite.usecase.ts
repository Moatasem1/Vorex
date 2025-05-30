import { inject, Inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import { IAddCryptoToFavoriteInputDto } from '../../../infrastructure/dtos/user.dto';
import { Observable } from 'rxjs';
import { UserRepository } from '../repositories/user-repository';
import { mapAddCryptoToFavoriteInputModelToDto } from '../mappers/user.mapper';

@Injectable({
  providedIn: 'root',
})
export class AddCryptoToFavouriteUseCase
  implements UseCase<IAddCryptoToFavoriteInputDto, boolean>
{
  private _userRepository = inject(UserRepository);
  execute(input: IAddCryptoToFavoriteInputDto): Observable<boolean> {
    return this._userRepository.addCryptoToFavourite(
      mapAddCryptoToFavoriteInputModelToDto(input)
    );
  }
}
