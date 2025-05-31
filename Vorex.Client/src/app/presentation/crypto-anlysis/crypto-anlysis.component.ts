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
import { roiTextClassColor } from '../../shared/helpers/crypto-anlysis.helper';
import { IOutput } from '../../application/abstraction/output';
import { HumanizeDaysPipe } from '../../shared/pipes/humanize-days.pipe';
import { AnalyzeRiskResultModalComponent } from './analyze-risk-result-modal/analyze-risk-result-modal.component';
import {
  IAnaylizeRiskResultModalInput,
  ICryptoHistoricalPriceModalInput,
} from './types/crypto.type';
import { CryptoHistoricalPricesModelComponent } from './crypto-historical-prices-model/crypto-historical-prices-model.component';
import { AddCryptoToFavouriteUseCase } from '../../application/users/use-cases/addCryptoToFavourite.usecase';
import { removeCryptoFromFavouriteUseCase } from '../../application/users/use-cases/removeCryptoFromFavourite.usecase';
import { IAddCryptoToFavoriteInput } from '../../application/users/models/user.model';
import { ToastrService } from '../../shared/services/toastr.service';
import { ActivatedRoute } from '@angular/router';

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
  private _activatedRouter = inject(ActivatedRoute);

  constructor() {
    this.paginationInput = {
      PageSize: 40,
      PageIndex: 1,
      SearchValue: '',
    };
  }

  ngOnInit() {
    this._activatedRouter.queryParams.subscribe((params) => {
      if (params['search']) {
        this.searchText = params['search'];
        this.updatePaginationInputSearchValue();
      }
      this.fetchCryptos();
    });
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

  scrollToTop() {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
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

  // favourite feature
  addCryptoToFavouriteUseCase = inject(AddCryptoToFavouriteUseCase);
  removeCryptoFromFavouriteUseCase = inject(removeCryptoFromFavouriteUseCase);
  private _toasterService = inject(ToastrService);
  addCryptoToFavourite(crypto: ICryptoListItem) {
    const input = { cryptoId: crypto.id } as IAddCryptoToFavoriteInput;
    this.addCryptoToFavouriteUseCase.execute(input).subscribe({
      next: () => {
        this._toasterService.success(
          '',
          `Crypto ${crypto.name} added to favourite`
        );
        this.filteredCryptos = this.filteredCryptos.map((c) => {
          if (c.id === crypto.id) {
            return { ...c, isFavourite: true };
          }
          return c;
        });
      },
      error: () => {},
    });
  }
  removeCryptoFromFavourite(crypto: ICryptoListItem) {
    this.removeCryptoFromFavouriteUseCase.execute(crypto.id).subscribe({
      next: () => {
        this._toasterService.success(
          '',
          `Crypto ${crypto.name} removed from favourite`
        );
        this.filteredCryptos = this.filteredCryptos.map((c) => {
          if (c.id === crypto.id) {
            return { ...c, isFavourite: false };
          }
          return c;
        });
      },
      error: () => {},
    });
  }

  toggleFavourite(isFavourite: boolean, crypto: ICryptoListItem) {
    isFavourite
      ? this.addCryptoToFavourite(crypto)
      : this.removeCryptoFromFavourite(crypto);
  }
}
