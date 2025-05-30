import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import { ICryptoAnlysisCompareItem } from '../models/user.model';
import { map, Observable } from 'rxjs';
import { UserRepository } from '../repositories/user-repository';
import { mapCryptoAnylysisHistoryCompareItemDtoToModel } from '../mappers/user.mapper';

@Injectable({
  providedIn: 'root',
})
export class GetAllCryptoAnlysisInCompareListUseCase
  implements UseCase<void, ICryptoAnlysisCompareItem[]>
{
  private _userRepository = inject(UserRepository);
  execute(input: void): Observable<ICryptoAnlysisCompareItem[]> {
    return this._userRepository
      .getAllCryptoAnlysisInCompare()
      .pipe(
        map((resp) =>
          resp.map((item) =>
            mapCryptoAnylysisHistoryCompareItemDtoToModel(item)
          )
        )
      );
  }
}
