import { inject, Injectable } from '@angular/core';
import { UseCase } from '../../abstraction/use-case';
import { UserRepository } from '../repositories/user-repository';
import { Observable } from 'rxjs';
import { IDeleteCryptoAnlysisHistoryRecordsInput } from '../models/user.model';
import { mapDeleteCryptoAnlysisHistoryRecordsInputModelToDto } from '../mappers/user.mapper';

@Injectable({
  providedIn: 'root',
})
export class DeleteCryptoAnlysisHistoryRecordsUseCase
  implements UseCase<IDeleteCryptoAnlysisHistoryRecordsInput, boolean>
{
  private _userRepository = inject(UserRepository);

  execute(input: IDeleteCryptoAnlysisHistoryRecordsInput): Observable<boolean> {
    return this._userRepository.deleteCryptoAnlysisHistoryRecords(
      mapDeleteCryptoAnlysisHistoryRecordsInputModelToDto(input)
    );
  }
}
