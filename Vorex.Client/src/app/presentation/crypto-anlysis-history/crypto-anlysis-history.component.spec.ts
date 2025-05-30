import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptoAnlysisHistoryComponent } from './crypto-anlysis-history.component';

describe('CryptoAnlysisHistoryComponent', () => {
  let component: CryptoAnlysisHistoryComponent;
  let fixture: ComponentFixture<CryptoAnlysisHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CryptoAnlysisHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CryptoAnlysisHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
