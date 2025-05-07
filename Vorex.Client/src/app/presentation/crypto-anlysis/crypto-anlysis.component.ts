import { Component, inject } from '@angular/core';
import { LucideAngularModule, Search, X } from 'lucide-angular';
import { FormsModule } from '@angular/forms';
import { EmptyResultComponent } from '../../shared/components/empty-result/empty-result.component';
import { GetPaginatedCryptosUseCase } from '../../application/cryptos/use-cases/get-paginated-Cryptos.usecase';
import { ICryptoListItem } from '../../application/cryptos/models/crypto.model';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-crypto-anlysis',
  imports: [LucideAngularModule, FormsModule, EmptyResultComponent, NgClass],
  templateUrl: './crypto-anlysis.component.html',
  styleUrl: './crypto-anlysis.component.scss',
})
export class CryptoAnlysisComponent {
  readonly Search = Search;
  readonly X = X;

  searchText: string = '';
  isSearchFocus: boolean = false;
  cryptos: ICryptoListItem[] = [];
  filteredCryptos: ICryptoListItem[] = [];

  //services
  GetPaginatedCryptosUseCase = inject(GetPaginatedCryptosUseCase);

  ngOnInit() {
    this.fetchCryptos();
  }

  fetchCryptos() {
    this.GetPaginatedCryptosUseCase.execute({
      PageSize: 10,
      PageIndex: 1,
      SearchValue: this.searchText,
    }).subscribe((cryptos) => {
      this.cryptos = cryptos;
      this.filteredCryptos = cryptos;
    });
  }

  toggleSearchFocus() {
    setTimeout(() => {
      this.isSearchFocus = !this.isSearchFocus;
      this.fetchCryptos();
    }, 500);
  }

  clearSearch = () => (this.searchText = '');
}
