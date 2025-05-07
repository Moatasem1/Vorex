import { inject, Injectable } from '@angular/core';
import { CryptoRepository } from '../../application/cryptos/repositories/crypto-repository';
import { Observable } from 'rxjs';
import { ICryptoListItemDto } from '../dtos/crypto.dto';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root',
})
export class CryptoApiRepository implements CryptoRepository {
  private _apiService = inject(ApiService);
  getAll(
    pageSize: number,
    pageIndex: number,
    searchValue?: string
  ): Observable<ICryptoListItemDto[]> {
    return this._apiService.get<ICryptoListItemDto[]>(
      `Crypto?PageSize=${pageSize}&PageIndex=${pageIndex}&SearchValue=${searchValue}`
    );
  }
}
