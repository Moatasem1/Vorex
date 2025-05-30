import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptoFavouriteCardComponent } from './crypto-favourite-card.component';

describe('CryptoFavouriteCardComponent', () => {
  let component: CryptoFavouriteCardComponent;
  let fixture: ComponentFixture<CryptoFavouriteCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CryptoFavouriteCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CryptoFavouriteCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
