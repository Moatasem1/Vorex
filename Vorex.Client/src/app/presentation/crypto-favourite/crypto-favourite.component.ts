import { Component, inject, signal } from '@angular/core';
import { IFavouriteCrypto } from '../../application/users/models/user.model';
import { EmptyResultComponent } from '../../shared/components/empty-result/empty-result.component';
import { CryptoFavouriteCardComponent } from './crypto-favourite-card/crypto-favourite-card.component';
import { GetFavouriteCryptosUseCase } from '../../application/users/use-cases/getFavouriteCrypto.usecase';
import { removeCryptoFromFavouriteUseCase } from '../../application/users/use-cases/removeCryptoFromFavourite.usecase';
import { ToastrService } from '../../shared/services/toastr.service';

@Component({
  selector: 'app-crypto-favourite',
  imports: [EmptyResultComponent, CryptoFavouriteCardComponent],
  templateUrl: './crypto-favourite.component.html',
  styleUrl: './crypto-favourite.component.scss',
})
export class CryptoFavouriteComponent {
  favouriteCrypto: IFavouriteCrypto[] = [];
  isGetCryptoFavouriteRequestLoading = signal(false);

  // services
  private getAllFavouriteCryptoUseCase = inject(GetFavouriteCryptosUseCase);
  private removeCryptoFromFavouriteUseCase = inject(
    removeCryptoFromFavouriteUseCase
  );
  private _toasterService = inject(ToastrService);

  ngOnInit() {
    this.fetchFavouriteCrypto();
  }

  fetchFavouriteCrypto() {
    this.isGetCryptoFavouriteRequestLoading.set(true);
    this.getAllFavouriteCryptoUseCase.execute().subscribe({
      next: (resp) => {
        this.isGetCryptoFavouriteRequestLoading.set(false);
        this.favouriteCrypto = resp;
      },
      error: (err) => {
        this.isGetCryptoFavouriteRequestLoading.set(false);
      },
    });
  }

  removeCryptoFromFavourite(crypto: IFavouriteCrypto) {
    this.removeCryptoFromFavouriteUseCase.execute(crypto.id).subscribe({
      next: () => {
        this.removeCryptoFromFavouriteFromUI(crypto.id);
        this._toasterService.success(
          '',
          `Crypto ${crypto.name} removed from favourite`
        );
      },
      error: () => {},
    });
  }

  removeCryptoFromFavouriteFromUI(cryptoId: string) {
    this.favouriteCrypto = this.favouriteCrypto.filter(
      (x) => x.id !== cryptoId
    );
  }
}
