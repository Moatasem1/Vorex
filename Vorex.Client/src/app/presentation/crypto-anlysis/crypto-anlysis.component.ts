import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { LucideAngularModule, Search, View, X } from 'lucide-angular';
import { FormsModule } from '@angular/forms';
import { EmptyResultComponent } from '../../shared/components/empty-result/empty-result.component';
import { GetPaginatedCryptosUseCase } from '../../application/cryptos/use-cases/get-paginated-Cryptos.usecase';
import {
  IAnalyzeRiskInput,
  IAnalyzeRiskResult,
  ICryptoHistoricalPrice,
  ICryptoListItem,
} from '../../application/cryptos/models/crypto.model';
import { JsonPipe, NgClass } from '@angular/common';
import {
  IBasicPaginatedInput,
  IPaginatedResponse,
} from '../../shared/types/shared.types';
import { PaginatorComponent } from '../../shared/components/paginator/paginator.component';
import { CryptoCardComponent } from './crypto-card/crypto-card.component';
import { PopupComponent } from '../../shared/components/popup/popup.component';
import { AnalyzeRiskModalComponent } from './analyze-risk-modal/analyze-risk-modal.component';
import { riskClassColor } from '../../shared/helpers/crypto-anlysis.helper';
import { IOutput } from '../../application/abstraction/output';
import { HumanizeDaysPipe } from '../../shared/pipes/humanize-days.pipe';
import { AnalyzeRiskResultModalComponent } from './analyze-risk-result-modal/analyze-risk-result-modal.component';
import {
  IAnaylizeRiskResultModalInput,
  ICryptoHistoricalPriceModalInput,
} from './types/crypto.type';
import { CryptoHistoricalPricesModelComponent } from './crypto-historical-prices-model/crypto-historical-prices-model.component';

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
    AnalyzeRiskResultModalComponent,
    CryptoHistoricalPricesModelComponent,
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
  riskAnaylized: IAnaylizeRiskResultModalInput | null = null;
  setSelectedCrypto(crypto: ICryptoListItem | null) {
    this.selectedCrypto = crypto;
  }

  setRiskAnaylizedResult(
    output: IOutput<IAnalyzeRiskInput, IAnalyzeRiskResult> | null
  ) {
    if (output == null) {
      this.riskAnaylized = null;
      return;
    }

    this.riskAnaylized = {
      cryptoName: this.selectedCrypto!.name,
      ...output.input,
      ...output.result,
    };
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

  // show risk analysis result feature
  @ViewChild(AnalyzeRiskResultModalComponent)
  riskAnalysisResultModal!: AnalyzeRiskResultModalComponent;
  showRiskAnalysisResultModal() {
    setTimeout(() => {
      this.riskAnalysisResultModal.model.show();
    }, 250);
  }

  // historical data feature
  @ViewChild(CryptoHistoricalPricesModelComponent)
  historicalDataModal!: CryptoHistoricalPricesModelComponent;
  historicalDataModalInput: ICryptoHistoricalPriceModalInput | null = null;

  showHistoricalDataModal() {
    setTimeout(() => {
      this.historicalDataModal.model.show();
    });
  }

  setHistoricalDataModalInput(crypto: ICryptoListItem | null) {
    if (crypto == null) {
      this.historicalDataModalInput = null;
      return;
    }
    this.historicalDataModalInput = {
      cryptoName: crypto.name,
      cryptoId: crypto.id,
    };
  }
}
