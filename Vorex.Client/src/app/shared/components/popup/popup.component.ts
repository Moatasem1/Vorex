import { Component, ElementRef, input, output, ViewChild } from '@angular/core';
import { Modal, ModalOptions } from 'flowbite';
import { LoaderComponent } from '../loader/loader.component';

@Component({
  selector: 'app-popup',
  imports: [LoaderComponent],
  templateUrl: './popup.component.html',
  styleUrl: './popup.component.scss',
})
export class PopupComponent {
  @ViewChild('modal') ModalEl!: ElementRef;
  title = input<string>('modal title');
  preventClose = input<boolean>(false);
  width = input<number>(600);
  popupClosed = output<void>();
  isPopupLoading = input<boolean>(false);

  modal: any;

  ngAfterViewInit() {
    const opstions: ModalOptions = {
      placement: 'center',
      backdrop: 'dynamic',
      backdropClasses: 'bg-gray-400 opacity-60 fixed z-40 inset-0',
      closable: false,
      onHide: () => console.log('Modal hidden'),
      onShow: () => console.log('Modal shown'),
      onToggle: () => console.log('Modal toggled'),
    };
    this.modal = new Modal(this.ModalEl.nativeElement, opstions);
  }

  show() {
    this.modal.show();
  }

  hide() {
    this.modal.hide();
    this.popupClosed.emit();
  }
}
