import { Component, input, ViewChild } from '@angular/core';
import { PopupComponent } from '../../../shared/components/popup/popup.component';
import { HumanizeDaysPipe } from '../../../shared/pipes/humanize-days.pipe';
import { NgClass } from '@angular/common';
import { IAnaylizeRiskResultModalInput } from '../types/crypto.type';
import { riskClassColor } from '../../../shared/helpers/crypto-anlysis.helper';

@Component({
  selector: 'app-analyze-risk-result-modal',
  imports: [PopupComponent, HumanizeDaysPipe, NgClass],
  templateUrl: './analyze-risk-result-modal.component.html',
  styleUrl: './analyze-risk-result-modal.component.scss',
})
export class AnalyzeRiskResultModalComponent {
  @ViewChild(PopupComponent) model!: PopupComponent;

  riskAnaylized = input.required<IAnaylizeRiskResultModalInput>();

  riskClassColor = riskClassColor;
}
