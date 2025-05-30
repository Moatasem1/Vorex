import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptoFavouriteComponent } from './crypto-favourite.component';

describe('CryptoFavouriteComponent', () => {
  let component: CryptoFavouriteComponent;
  let fixture: ComponentFixture<CryptoFavouriteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CryptoFavouriteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CryptoFavouriteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
