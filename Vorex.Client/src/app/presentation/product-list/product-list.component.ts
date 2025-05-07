import { Component, inject } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { GetAllProductsUseCase } from '../../application/cryptos/use-cases/get-all-products.usecase';
import { ProductListItem } from '../../application/cryptos/models/product.model';

@Component({
  selector: 'app-product-list',
  imports: [CurrencyPipe],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
})
export class ProductListComponent {
  products: ProductListItem[] = [];

  getAllProductUseCase = inject(GetAllProductsUseCase);

  ngOnInit() {
    this.getAllProductUseCase.execute().subscribe((products) => {
      this.products = products;
    });
  }
}
