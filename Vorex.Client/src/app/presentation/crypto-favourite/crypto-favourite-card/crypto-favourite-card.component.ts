import { Component, input, output } from '@angular/core';
import { IFavouriteCrypto } from '../../../application/users/models/user.model';
import { LucideAngularModule, X } from 'lucide-angular';

@Component({
  selector: 'app-crypto-favourite-card',
  imports: [LucideAngularModule],
  templateUrl: './crypto-favourite-card.component.html',
  styleUrl: './crypto-favourite-card.component.scss',
})
export class CryptoFavouriteCardComponent {
  readonly X = X;
  crypto = input.required<IFavouriteCrypto>();
  cryptoDeleted = output<IFavouriteCrypto>();

  emitCryptoDeleted() {
    this.cryptoDeleted.emit(this.crypto());
  }
}
