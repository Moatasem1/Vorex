import { Component, input, ViewChild } from '@angular/core';
import { PopupComponent } from '../popup/popup.component';
import { CircleAlert, LogIn, LucideAngularModule } from 'lucide-angular';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-plz-login-modal',
  imports: [PopupComponent, LucideAngularModule, RouterLink],
  templateUrl: './plz-login-modal.component.html',
  styleUrl: './plz-login-modal.component.scss',
})
export class PlzLoginModalComponent {
  // icons
  readonly CircleAlert = CircleAlert;
  @ViewChild(PopupComponent) modal!: PopupComponent;
  message = input<string>('');
}
