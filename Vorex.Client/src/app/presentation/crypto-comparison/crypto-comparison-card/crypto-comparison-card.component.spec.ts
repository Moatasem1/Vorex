import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptoComparisonCardComponent } from './crypto-comparison-card.component';

describe('CryptoComparisonCardComponent', () => {
  let component: CryptoComparisonCardComponent;
  let fixture: ComponentFixture<CryptoComparisonCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CryptoComparisonCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CryptoComparisonCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
