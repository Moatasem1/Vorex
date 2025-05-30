import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import { IAddCryptoAnlysisToCompareInput } from '../models/user.model';
import { Observable } from 'rxjs';
import { UserRepository } from '../repositories/user-repository';
import { mapAddCryptoAnlysisToCompareInputModelToDto } from '../mappers/user.mapper';

@Injectable({
  providedIn: 'root',
})
export class AddCryptoAnlysisToCompareUseCase
  implements UseCase<IAddCryptoAnlysisToCompareInput, boolean>
{
  private _userRepository = inject(UserRepository);
  execute(input: IAddCryptoAnlysisToCompareInput): Observable<boolean> {
    return this._userRepository.addCryptoAnlysisToCompare(
      mapAddCryptoAnlysisToCompareInputModelToDto(input)
    );
  }
}
