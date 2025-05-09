import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptoHistoricalPricesModelComponent } from './crypto-historical-prices-model.component';

describe('CryptoHistoricalPricesModelComponent', () => {
  let component: CryptoHistoricalPricesModelComponent;
  let fixture: ComponentFixture<CryptoHistoricalPricesModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CryptoHistoricalPricesModelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CryptoHistoricalPricesModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
