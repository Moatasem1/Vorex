import {
  Component,
  ElementRef,
  EventEmitter,
  input,
  Input,
  OnChanges,
  Output,
  ViewChild,
} from '@angular/core';
import { Modal, ModalInterface, ModalOptions } from 'flowbite';

@Component({
  selector: 'app-popup',
  imports: [],
  templateUrl: './popup.component.html',
  styleUrl: './popup.component.scss',
})
export class PopupComponent {
  @ViewChild('modal') ModalEl!: ElementRef;
  title = input<string>('modal title');
  preventClose = input<boolean>(false);

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
  }
}
