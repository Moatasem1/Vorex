import { NgClass } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { LucideAngularModule, Trash2 } from 'lucide-angular';
import {
  riskBackgroundClassColor,
  riskTextClassColor,
} from '../../../shared/helpers/crypto-anlysis.helper';
import { HumanizeDaysPipe } from '../../../shared/pipes/humanize-days.pipe';
import { ICryptoAnlysisCompareItem } from '../../../application/users/models/user.model';

@Component({
  selector: 'app-crypto-comparison-card',
  imports: [LucideAngularModule, NgClass, HumanizeDaysPipe],
  templateUrl: './crypto-comparison-card.component.html',
  styleUrl: './crypto-comparison-card.component.scss',
})
export class CryptoComparisonCardComponent {
  readonly Trash2 = Trash2;
  input = input.required<ICryptoAnlysisCompareItem>();
  removedFromComparison = output<string>();

  // services
  riskClassColor = riskBackgroundClassColor;

  emitRemovedFromComparison() {
    this.removedFromComparison.emit(this.input().cryptoAnlysisHistoryId);
  }
}
