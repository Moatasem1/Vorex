import { NgClass } from '@angular/common';
import { Component, input } from '@angular/core';
import { Info, LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-empty-result',
  imports: [LucideAngularModule, NgClass],
  templateUrl: './empty-result.component.html',
  styleUrl: './empty-result.component.scss',
})
export class EmptyResultComponent {
  readonly Info = Info;
  searchText = input<string>('no data found');
  entityName = input<string>('data');
  showBorder = input<boolean>(true);
  fullMessage = input<string>('');
}
