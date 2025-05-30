import { Component, input, output } from '@angular/core';
import { ICryptoListItem } from '../../../application/cryptos/models/crypto.model';
import { CommonModule, NgClass } from '@angular/common';
import { Heart, LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-crypto-card',
  imports: [NgClass, LucideAngularModule, CommonModule],
  templateUrl: './crypto-card.component.html',
  styleUrl: './crypto-card.component.scss',
  animations: [],
})
export class CryptoCardComponent {
  readonly Heart = Heart;
  crypto = input.required<ICryptoListItem>();
  anayzeRiskClicked = output<ICryptoListItem>();
  historicalDataClicked = output<ICryptoListItem>();
  fovouriteClicked = output<boolean>();
  isHovered = false;

  toggleHovered = () => (this.isHovered = !this.isHovered);

  emitAnalyzeRisk = () => this.anayzeRiskClicked.emit(this.crypto());

  emitHistoricalData = () => this.historicalDataClicked.emit(this.crypto());

  toggleFavourite = () =>
    (this.crypto().isFavourite = !this.crypto().isFavourite);

  emitFavouriteClicked = () =>
    this.fovouriteClicked.emit(this.crypto().isFavourite);
}
