import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteCryptoAnlysisHistoryModalComponent } from './delete-crypto-anlysis-history-modal.component';

describe('DeleteCryptoAnlysisHistoryModalComponent', () => {
  let component: DeleteCryptoAnlysisHistoryModalComponent;
  let fixture: ComponentFixture<DeleteCryptoAnlysisHistoryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteCryptoAnlysisHistoryModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteCryptoAnlysisHistoryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
