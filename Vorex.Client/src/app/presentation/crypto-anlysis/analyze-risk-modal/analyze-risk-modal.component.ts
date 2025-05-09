import {
  Component,
  inject,
  input,
  Output,
  output,
  signal,
  ViewChild,
} from '@angular/core';
import { PopupComponent } from '../../../shared/components/popup/popup.component';
import {
  IAnalyzeRiskInput,
  IAnalyzeRiskResult,
  ICryptoListItem,
} from '../../../application/cryptos/models/crypto.model';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AnaylizeRiskUseCase } from '../../../application/cryptos/use-cases/anaylize-risk.usecase';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';
import { IOutput } from '../../../application/abstraction/output';

type AnalyzeRiskInputFormControls = {
  amount: FormControl<number>;
  holdingTimeOption: FormControl<string>;
  holdingTime: FormControl<number>;
};

@Component({
  selector: 'app-analyze-risk-modal',
  imports: [PopupComponent, ReactiveFormsModule, LoaderComponent],
  templateUrl: './analyze-risk-modal.component.html',
  styleUrl: './analyze-risk-modal.component.scss',
})
export class AnalyzeRiskModalComponent {
  @ViewChild(PopupComponent) model!: PopupComponent;
  crypto = input.required<ICryptoListItem>();
  riskAnaylized = output<IOutput<IAnalyzeRiskInput, IAnalyzeRiskResult>>();
  riskForm!: FormGroup<AnalyzeRiskInputFormControls>;
  holdingTimeOptions = ['day', 'week', 'month', 'year'];
  isAnalyzingRiskLoading = signal(false);

  // services
  private _formBuilder = inject(FormBuilder);
  private _analyzeRiskUseCase = inject(AnaylizeRiskUseCase);

  ngOnInit() {
    this.initiliazeRiskForm();
  }

  initiliazeRiskForm() {
    this.riskForm = this._formBuilder.group<AnalyzeRiskInputFormControls>({
      amount: this._formBuilder.nonNullable.control(0, [
        Validators.required,
        Validators.min(1),
      ]),
      holdingTimeOption: this._formBuilder.nonNullable.control('week', [
        Validators.required,
      ]),
      holdingTime: this._formBuilder.nonNullable.control(0, [
        Validators.required,
        Validators.min(1),
      ]),
    });
  }

  analyzeRisk() {
    this.isAnalyzingRiskLoading.set(true);
    const analyzeRiskInput: IAnalyzeRiskInput = {
      cryptoId: this.crypto().id,
      investmentAmount: this.riskForm.controls.amount.value,
      holdingDays: this.calcualteHoldingDays(),
    };
    this._analyzeRiskUseCase.execute(analyzeRiskInput).subscribe({
      next: (result) => {
        this.isAnalyzingRiskLoading.set(false);
        this.riskAnaylized.emit({ input: analyzeRiskInput, result: result });
      },
      error: () => {
        this.isAnalyzingRiskLoading.set(false);
        this.model.hide();
      },
    });
  }

  calcualteHoldingDays(): number {
    switch (this.riskForm.controls.holdingTimeOption.value) {
      case this.holdingTimeOptions[0]:
        return this.riskForm.controls.holdingTime.value;
      case this.holdingTimeOptions[1]:
        return this.riskForm.controls.holdingTime.value * 7;
      case this.holdingTimeOptions[2]:
        return this.riskForm.controls.holdingTime.value * 30;
      case this.holdingTimeOptions[3]:
        return this.riskForm.controls.holdingTime.value * 365;
      default:
        return 0;
    }
  }

  resetForm() {
    this.riskForm.reset();
  }
}
