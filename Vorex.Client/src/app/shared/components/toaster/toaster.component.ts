import { NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  CircleX,
  Info,
  LucideAngularModule,
  LucideIconData,
  ShieldCheck,
  TriangleAlert,
  X,
} from 'lucide-angular';
import { IToast, ToastType } from '../../types/shared.types';
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from '@angular/animations';
import { ToastrService } from '../../services/toastr.service';
import { Subscription } from 'rxjs';

interface IToastUI extends IToast {
  icon: LucideIconData;
  colorClass: string;
}

@Component({
  selector: 'app-toaster',
  imports: [NgClass, LucideAngularModule],
  templateUrl: './toaster.component.html',
  styleUrl: './toaster.component.scss',
  animations: [
    trigger('toastAnimation', [
      transition(':enter', [
        style({
          transform: 'translateY(-20%) translateX(100%)',
          opacity: 0,
        }),
        animate(
          '300ms ease-out',
          style({
            transform: 'translateY(0) translateX(0)',
            opacity: 1,
          })
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({
            transform: 'translateY(-20%) translateX(100%)',
            opacity: 0,
          })
        ),
      ]),
    ]),
  ],
})
export class ToasterComponent {
  readonly Info = Info;
  readonly TriangleAlert = TriangleAlert;
  readonly CircleX = CircleX;
  readonly ShieldCheck = ShieldCheck;
  readonly X = X;
  ToastType = ToastType;
  private _toasterService = inject(ToastrService);
  toasts: IToastUI[] = [];
  private subscription: Subscription = new Subscription();

  ngOnInit() {
    this.subscription = this._toasterService.toasts$.subscribe((toasts) => {
      this.toasts = toasts.map((toast) => ({
        ...toast,
        icon: this.getIcon(toast.type),
        colorClass: this.getClassColor(toast.type),
      }));
    });
  }

  ngOnDestroy() {
    if (this.subscription) this.subscription.unsubscribe();
  }

  removeToast(id: string) {
    this._toasterService.removeToast(id);
  }

  getClassColor(type: ToastType) {
    switch (type) {
      case ToastType.Success:
        return 'toast--success';
      case ToastType.Error:
        return 'toast--error';
      case ToastType.Warning:
        return 'toast--warning';
      case ToastType.Info:
        return 'toast--info';
    }
  }

  getIcon(type: ToastType) {
    switch (type) {
      case ToastType.Success:
        return this.ShieldCheck;
      case ToastType.Error:
        return this.TriangleAlert;
      case ToastType.Warning:
        return this.CircleX;
      case ToastType.Info:
        return this.Info;
    }
  }
}
