import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptoComparisonComponent } from './crypto-comparison.component';

describe('CryptoComparisonComponent', () => {
  let component: CryptoComparisonComponent;
  let fixture: ComponentFixture<CryptoComparisonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CryptoComparisonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CryptoComparisonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
