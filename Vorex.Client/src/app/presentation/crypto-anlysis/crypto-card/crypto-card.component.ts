import { Component, input, output } from '@angular/core';
import { ICryptoListItem } from '../../../application/cryptos/models/crypto.model';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-crypto-card',
  imports: [NgClass],
  templateUrl: './crypto-card.component.html',
  styleUrl: './crypto-card.component.scss',
  animations: [],
})
export class CryptoCardComponent {
  crypto = input.required<ICryptoListItem>();
  anayzeRiskClicked = output<ICryptoListItem>();
  historicalDataClicked = output<ICryptoListItem>();
  isHovered = false;

  toggleHovered = () => (this.isHovered = !this.isHovered);

  emitAnalyzeRisk = () => this.anayzeRiskClicked.emit(this.crypto());

  emitHistoricalData = () => this.historicalDataClicked.emit(this.crypto());
}
