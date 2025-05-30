import { NgClass } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
  Output,
  ViewChild,
} from '@angular/core';

@Component({
  selector: 'app-dropdown',
  imports: [NgClass],
  templateUrl: './dropdown.component.html',
  styleUrl: './dropdown.component.scss',
})
export class DropdownComponent {
  @Input() MenuVisible = false;
  @Output() MenuVisibleChange = new EventEmitter<boolean>();
  @ViewChild('dropdown') dropdown!: ElementRef;

  toggleDropdown() {
    this.MenuVisible = !this.MenuVisible;
  }

  closeDropdown() {
    this.MenuVisible = false;
  }

  emitDropdownState() {
    this.MenuVisibleChange.emit(this.MenuVisible);
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    let clickedInside = this.dropdown.nativeElement.contains(event.target);
    if (this.MenuVisible && !clickedInside) {
      this.closeDropdown();
    }
  }
}
