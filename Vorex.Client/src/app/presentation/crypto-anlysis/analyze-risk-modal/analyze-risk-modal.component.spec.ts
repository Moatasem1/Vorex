import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnalyzeRiskModalComponent } from './analyze-risk-modal.component';

describe('AnalyzeRiskModalComponent', () => {
  let component: AnalyzeRiskModalComponent;
  let fixture: ComponentFixture<AnalyzeRiskModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AnalyzeRiskModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnalyzeRiskModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
