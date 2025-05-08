import { Component, inject, ViewChild } from '@angular/core';
import { LucideAngularModule, Search, View, X } from 'lucide-angular';
import { FormsModule } from '@angular/forms';
import { EmptyResultComponent } from '../../shared/components/empty-result/empty-result.component';
import { GetPaginatedCryptosUseCase } from '../../application/cryptos/use-cases/get-paginated-Cryptos.usecase';
import {
  IAnalyzeRiskResult,
  ICryptoListItem,
} from '../../application/cryptos/models/crypto.model';
import { NgClass } from '@angular/common';
import {
  IBasicPaginatedInput,
  IPaginatedResponse,
} from '../../shared/types/shared.types';
import { PaginatorComponent } from '../../shared/components/paginator/paginator.component';
import { CryptoCardComponent } from './crypto-card/crypto-card.component';
import { PopupComponent } from '../../shared/components/popup/popup.component';
import { AnalyzeRiskModalComponent } from './analyze-risk-modal/analyze-risk-modal.component';

@Component({
  selector: 'app-crypto-anlysis',
  imports: [
    LucideAngularModule,
    FormsModule,
    EmptyResultComponent,
    NgClass,
    PaginatorComponent,
    CryptoCardComponent,
    AnalyzeRiskModalComponent,
    PopupComponent,
  ],
  templateUrl: './crypto-anlysis.component.html',
  styleUrl: './crypto-anlysis.component.scss',
})
export class CryptoAnlysisComponent {
  @ViewChild(AnalyzeRiskModalComponent)
  anayzeRiskModal!: AnalyzeRiskModalComponent;
  readonly Search = Search;
  readonly X = X;

  searchText: string = '';
  isSearchFocus: boolean = false;
  paginatedCryptos?: IPaginatedResponse<ICryptoListItem[]>;
  paginationInput!: IBasicPaginatedInput;
  filteredCryptos: ICryptoListItem[] = [];

  //services
  GetPaginatedCryptosUseCase = inject(GetPaginatedCryptosUseCase);

  constructor() {
    this.paginationInput = {
      PageSize: 20,
      PageIndex: 1,
      SearchValue: '',
    };
  }

  ngOnInit() {
    this.fetchCryptos();
  }

  fetchCryptos() {
    this.GetPaginatedCryptosUseCase.execute(this.paginationInput).subscribe(
      (resp) => {
        this.paginatedCryptos = resp;
        this.filteredCryptos = resp.data;
      }
    );
  }

  toggleSearchFocus() {
    setTimeout(() => {
      this.isSearchFocus = !this.isSearchFocus;
      this.fetchCryptos();
    }, 500);
  }

  updatePaginationInputPage(page: number) {
    this.paginationInput.PageIndex = page;
  }

  updatePaginationInputSearchValue() {
    this.paginationInput.SearchValue = this.searchText;
  }

  clearSearch = () => (this.searchText = '');

  // risk analysis feature
  selectedCrypto: ICryptoListItem | null = null;
  riskAnaylizedResult: IAnalyzeRiskResult | null = null;
  setSelectedCrypto(crypto: ICryptoListItem | null) {
    this.selectedCrypto = crypto;
  }

  setRiskAnaylizedResult(result: IAnalyzeRiskResult | null) {
    this.riskAnaylizedResult = result;
  }

  showAnlysisRiskModal() {
    setTimeout(() => {
      this.anayzeRiskModal.model.show();
    });
  }

  hideAnlysisRiskModal() {
    this.anayzeRiskModal.model.hide();
    this.anayzeRiskModal.resetForm();
  }

  // show risk analysis result
  @ViewChild(PopupComponent) riskAnalysisResultModal!: PopupComponent;
  showRiskAnalysisResultModal() {
    setTimeout(() => {
      this.riskAnalysisResultModal.show();
    }, 250);
  }
}
