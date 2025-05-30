import { Component, inject, signal } from '@angular/core';
import { LucideAngularModule, Mail } from 'lucide-angular';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';
import { ToastrService } from '../../../shared/services/toastr.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { VerifyEmailUseCase } from '../../../application/auth/usecases/verify-email.usecase';
import { IVerfiyAccountEmailInput } from '../../../application/auth/models/auth.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-email-confirmation',
  imports: [LucideAngularModule, LoaderComponent],
  templateUrl: './email-confirmation.component.html',
  styleUrl: './email-confirmation.component.scss',
})
export class EmailConfirmationComponent {
  readonly Mail = Mail;
  isConfirmEmailRequestLoading = signal(false);
  isConfirmEmailRequestError = signal(false);
  isConfirmEmailRequestSuccess = signal(false);

  // service
  private _taosterService = inject(ToastrService);
  private _activatedRoute = inject(ActivatedRoute);
  private _verifyEmailUseCase = inject(VerifyEmailUseCase);
  private _router = inject(Router);

  ngOnInit() {
    this._activatedRoute.queryParams.subscribe((params) => {
      if (params['token']) {
        console.log('token,', params['token']);
        this.confirmEmail(params['token']);
        return;
      }
      this._router.navigate(['/auth/login']);
    });
  }

  confirmEmail(token: string) {
    const input = { token: token } as IVerfiyAccountEmailInput;

    this.isConfirmEmailRequestLoading.set(true);
    this._verifyEmailUseCase.execute(input).subscribe({
      next: () => {
        this.isConfirmEmailRequestLoading.set(false);
        this.isConfirmEmailRequestError.set(false);
        this.isConfirmEmailRequestSuccess.set(true);
        this._taosterService.success(
          'welcome to vorex family',
          'email confirmed successfully'
        );
        setTimeout(() => {
          this._router.navigate(['/auth/login']);
        }, 2000);
      },
      error: () => {
        this.isConfirmEmailRequestLoading.set(false);
        this.isConfirmEmailRequestError.set(true);
        this.isConfirmEmailRequestSuccess.set(false);
        setTimeout(() => {
          this._router.navigate(['/auth/login']);
        }, 2000);
      },
    });
  }
}
