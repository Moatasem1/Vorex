import { CommonModule, NgClass } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  Output,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-paginator',
  imports: [NgClass, CommonModule],
  templateUrl: './paginator.component.html',
  styleUrl: './paginator.component.scss',
})
export class PaginatorComponent {
  @Input() totalPages: number = 10;
  @Input() visibleBubbles: number = 5;
  @Output() pageChange = new EventEmitter<number>();

  @Input() currentPage: number = 1;
  visiblePages: number[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['totalPages'] || changes['visibleBubbles']) {
      this.calculateVisiblePages();
    }
  }

  ngOnInit(): void {
    this.calculateVisiblePages();
  }

  handlePageChange(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.calculateVisiblePages();
    this.pageChange.emit(this.currentPage);
  }

  private calculateVisiblePages(): void {
    // Ensure visibleBubbles doesn't exceed totalPages
    const adjustedVisibleBubbles = Math.min(
      this.visibleBubbles,
      this.totalPages
    );

    // If we have fewer total pages than the requested visible bubbles
    if (this.totalPages <= adjustedVisibleBubbles) {
      this.visiblePages = Array.from(
        { length: this.totalPages },
        (_, i) => i + 1
      );
      return;
    }

    const halfVisible = Math.floor(adjustedVisibleBubbles / 2);

    // Handle cases near the start
    if (this.currentPage <= halfVisible + 1) {
      this.visiblePages = Array.from(
        { length: adjustedVisibleBubbles },
        (_, i) => i + 1
      );
      return;
    }

    // Handle cases near the end
    if (this.currentPage >= this.totalPages - halfVisible) {
      this.visiblePages = Array.from(
        { length: adjustedVisibleBubbles },
        (_, i) => this.totalPages - adjustedVisibleBubbles + i + 1
      );
      return;
    }

    // Handle middle cases - center the current page
    const start = this.currentPage - halfVisible;
    this.visiblePages = Array.from(
      { length: adjustedVisibleBubbles },
      (_, i) => start + i
    );
  }
}
