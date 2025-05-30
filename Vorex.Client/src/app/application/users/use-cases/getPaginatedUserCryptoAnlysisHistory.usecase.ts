import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import {
  IBasicPaginatedInput,
  IDatePaginatedInput,
  IPaginatedResponse,
} from '../../../shared/types/shared.types';
import { ICryptoAnylysisHistory } from '../../users/models/user.model';
import { UserRepository } from '../repositories/user-repository';
import { map, Observable } from 'rxjs';
import { mapCryptoAnylysisHistoryDtoToModel } from '../mappers/user.mapper';

@Injectable({
  providedIn: 'root',
})
export class GetPaginatedUserCryptoAnlysisHistoryUseCase
  implements
    UseCase<IDatePaginatedInput, IPaginatedResponse<ICryptoAnylysisHistory[]>>
{
  private _userRepository = inject(UserRepository);

  execute(
    input: IDatePaginatedInput
  ): Observable<IPaginatedResponse<ICryptoAnylysisHistory[]>> {
    return this._userRepository
      .getAllCryptoAnlysisHistory(
        input.PageSize,
        input.PageIndex,
        input.SearchValue?.trim(),
        input.startDate?.toString(),
        input.endDate?.toString()
      )
      .pipe(
        map((resp) => {
          return {
            ...resp,
            data: resp.data.map(mapCryptoAnylysisHistoryDtoToModel),
          };
        })
      );
  }
}
