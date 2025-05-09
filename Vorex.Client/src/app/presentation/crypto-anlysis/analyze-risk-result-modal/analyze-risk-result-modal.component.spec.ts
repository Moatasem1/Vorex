import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnalyzeRiskResultModalComponent } from './analyze-risk-result-modal.component';

describe('AnalyzeRiskResultModalComponent', () => {
  let component: AnalyzeRiskResultModalComponent;
  let fixture: ComponentFixture<AnalyzeRiskResultModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AnalyzeRiskResultModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnalyzeRiskResultModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
