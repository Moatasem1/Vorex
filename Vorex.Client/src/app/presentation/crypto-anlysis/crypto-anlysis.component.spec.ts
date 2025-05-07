import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptoAnlysisComponent } from './crypto-anlysis.component';

describe('CryptoAnlysisComponent', () => {
  let component: CryptoAnlysisComponent;
  let fixture: ComponentFixture<CryptoAnlysisComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CryptoAnlysisComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CryptoAnlysisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
