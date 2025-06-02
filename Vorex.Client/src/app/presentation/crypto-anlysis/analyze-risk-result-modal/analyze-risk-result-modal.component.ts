import { Component, inject, input, signal, ViewChild } from '@angular/core';
import { PopupComponent } from '../../../shared/components/popup/popup.component';
import { HumanizeDaysPipe } from '../../../shared/pipes/humanize-days.pipe';
import { NgClass } from '@angular/common';
import { IAnaylizeRiskResultModalInput } from '../types/crypto.type';
import { roiTextClassColor } from '../../../shared/helpers/crypto-anlysis.helper';
import { AddCryptoAnlysisToCompareUseCase } from '../../../application/users/use-cases/addCryptoAnlysisToCompare.usecase';
import { IAddCryptoAnlysisToCompareInput } from '../../../application/users/models/user.model';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';
import { ToastrService } from '../../../shared/services/toastr.service';
import { AuthService } from '../../../shared/services/auth.service';

@Component({
  selector: 'app-analyze-risk-result-modal',
  imports: [PopupComponent, HumanizeDaysPipe, NgClass, LoaderComponent],
  templateUrl: './analyze-risk-result-modal.component.html',
  styleUrl: './analyze-risk-result-modal.component.scss',
})
export class AnalyzeRiskResultModalComponent {
  @ViewChild(PopupComponent) model!: PopupComponent;

  riskAnaylized = input.required<IAnaylizeRiskResultModalInput>();
  isAddToCompareRequestLoading = signal(false);

  riskClassColor = roiTextClassColor;

  // sevices
  private _addToCompareUseCase = inject(AddCryptoAnlysisToCompareUseCase);
  private _toastService = inject(ToastrService);
  private _authService = inject(AuthService);

  addToCompare() {
    if (!this._authService.isAuthenticated()) {
      this._toastService.error(
        'please login first',
        'to add analysis result to compare'
      );
      return;
    }

    let input = {
      cryptoAnlysisHistoryIds: [this.riskAnaylized().cryptoAnalysisHistoryId],
    } as IAddCryptoAnlysisToCompareInput;

    this.isAddToCompareRequestLoading.set(true);
    this._addToCompareUseCase.execute(input).subscribe({
      next: () => {
        this.isAddToCompareRequestLoading.set(false);
        this.model.hide();
        this._toastService.success('', 'added to compare successfully');
      },
      error: () => {
        this.isAddToCompareRequestLoading.set(false);
        this.model.hide();
      },
    });
  }
}
