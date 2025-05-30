import { NgClass } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  input,
  Output,
  output,
  signal,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LucideAngularModule, Search, X } from 'lucide-angular';

@Component({
  selector: 'app-search-bar',
  imports: [LucideAngularModule, FormsModule, NgClass],
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.scss',
})
export class SearchBarComponent {
  readonly Search = Search;
  readonly X = X;
  @Input() searchText: string = '';
  @Output() searchTextChange = new EventEmitter<string>();
  size = input<'sm' | 'md' | 'lg'>('md');
  isSearchFocus = signal<boolean>(false);
  searchCleared = output<void>();

  toggleSearchFocus = () =>
    setTimeout(() => {
      this.isSearchFocus.set(!this.isSearchFocus());
    }, 300);

  clearSearchText = () => (this.searchText = '');

  clearSearch() {
    this.searchText = '';
  }

  emitSearchText = () => this.searchTextChange.emit(this.searchText);

  emitSearchCleared = () => this.searchCleared.emit();
}
