import { Injectable } from '@angular/core';
import { IToast, ToastType } from '../types/shared.types';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ToastrService {
  private _toasts: IToast[] = [];
  private _toastSubject = new BehaviorSubject<IToast[]>([]);

  toasts$ = this._toastSubject.asObservable();

  private show(title: string, message: string, type: ToastType) {
    const id = Date.now().toString();
    this._toasts.push({ id, title, message, type });
    this._toastSubject.next(this._toasts);
    setTimeout(() => this.removeToast(id), 4000);

    return id;
  }

  removeToast(id: string) {
    this._toasts = this._toasts.filter((toast) => toast.id !== id);
    this._toastSubject.next(this._toasts);
  }

  success(title: string, message: string) {
    return this.show(title, message, ToastType.Success);
  }

  error(title: string, message: string) {
    return this.show(title, message, ToastType.Error);
  }

  warning(title: string, message: string) {
    return this.show(title, message, ToastType.Warning);
  }

  info(title: string, message: string) {
    return this.show(title, message, ToastType.Info);
  }
}
