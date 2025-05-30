import { Component, inject, signal } from '@angular/core';
import { LucideAngularModule, Trash, Trash2 } from 'lucide-angular';
import { CryptoComparisonCardComponent } from './crypto-comparison-card/crypto-comparison-card.component';
import { ICryptoAnlysisCompareItem } from '../../application/users/models/user.model';
import { EmptyResultComponent } from '../../shared/components/empty-result/empty-result.component';
import { GetAllCryptoAnlysisInCompareListUseCase } from '../../application/users/use-cases/getAllCryptoAnlysisInCompareList.usecase';
import { RemoveCryptoAnlysisFromCompareUseCase } from '../../application/users/use-cases/removeCryptoAnlysisFromCompare.usecase';
import { ToastrService } from '../../shared/services/toastr.service';

@Component({
  selector: 'app-crypto-comparison',
  imports: [
    LucideAngularModule,
    CryptoComparisonCardComponent,
    EmptyResultComponent,
  ],
  templateUrl: './crypto-comparison.component.html',
  styleUrl: './crypto-comparison.component.scss',
})
export class CryptoComparisonComponent {
  readonly Trash = Trash2;

  cryptoCompares: ICryptoAnlysisCompareItem[] = [];
  isFetchComparesRequestLoading = signal(false);

  // services
  private _getAllCryptoAnlysisInCompareUseCase = inject(
    GetAllCryptoAnlysisInCompareListUseCase
  );
  private _deleteCryptoAnalysisHistoryRecordsUseCase = inject(
    RemoveCryptoAnlysisFromCompareUseCase
  );

  private _toastService = inject(ToastrService);

  ngOnInit() {
    // this.fetchCompares();
    this.cryptoCompares = [
      {
        cryptoAnlysisHistoryId: '1',
        cryptoName: 'Bitcoin',
        investAmount: 1000,
        holdingDays: 60,
        risk: 5,
      },
      {
        cryptoAnlysisHistoryId: '2',
        cryptoName: 'Ethereum',
        investAmount: 1500,
        holdingDays: 60,
        risk: 60,
      },
      {
        cryptoAnlysisHistoryId: '3',
        cryptoName: 'Litecoin',
        investAmount: 800,
        holdingDays: 60,
        risk: 10,
      },
      {
        cryptoAnlysisHistoryId: '4',
        cryptoName: 'Ripple',
        investAmount: 1200,
        holdingDays: 60,
        risk: 90,
      },
    ] as ICryptoAnlysisCompareItem[];
  }

  fetchCompares() {
    this.isFetchComparesRequestLoading.set(true);
    this._getAllCryptoAnlysisInCompareUseCase.execute().subscribe({
      next: (resp) => {
        this.isFetchComparesRequestLoading.set(false);
        this.cryptoCompares = resp;
      },
      error: (err) => {
        this.isFetchComparesRequestLoading.set(false);
      },
    });
  }

  removeFromCompare(cryptoAnlysisHistoryId: string) {
    this._deleteCryptoAnalysisHistoryRecordsUseCase
      .execute(cryptoAnlysisHistoryId)
      .subscribe({
        next: () => {
          this._toastService.success('', 'Removed from compare successfully');
        },
        error: () => {},
      });
  }
}
