import { Component, inject, OnInit, signal } from '@angular/core';
import { authcoverImages } from '../auth.constant';
import { CommonModule, NgStyle } from '@angular/common';
import { LucideAngularModule, TrendingUp } from 'lucide-angular';
import { IError } from '../../../shared/types/shared.types';
import { ErrorType } from '../../../shared/constants/shared.constants';
import {
  ICreateAccountInput,
  ILoginInput,
  ILoginResponse,
} from '../../../application/auth/models/auth.model';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CreateAccountUseCase } from '../../../application/auth/usecases/create-account.usecase';
import { ToastrService } from '../../../shared/services/toastr.service';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';
import { LoginUseCase } from '../../../application/auth/usecases/login.usecase';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../shared/services/auth.service';
import { ILoginForm } from '../auth.types';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    LucideAngularModule,
    LoaderComponent,
    ReactiveFormsModule,
    RouterLink,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  // icons
  readonly TrendingUp = TrendingUp;
  coverImages = authcoverImages;
  currentCoverImageIndex = 0;

  loginForm!: FormGroup<ILoginForm>;
  isLoginRequestLoading = signal(false);
  errorMessage = signal('');
  // services
  private _formBuilderService = inject(FormBuilder);
  private _loginUseCase = inject(LoginUseCase);
  private _toastService = inject(ToastrService);
  private _authService = inject(AuthService);
  private _router = inject(Router);

  constructor() {}

  ngOnInit() {
    this.initializeLoginForm();
    this.playImages();
  }

  playImages() {
    setInterval(() => {
      this.currentCoverImageIndex =
        (this.currentCoverImageIndex + 1) % this.coverImages.length;
    }, 5000);
  }

  initializeLoginForm() {
    this.loginForm = this._formBuilderService.nonNullable.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(20),
        ],
      ],
    });
  }

  login() {
    const input = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    } as ILoginInput;

    this.isLoginRequestLoading.set(true);
    this.errorMessage.set('');
    this._loginUseCase.execute(input).subscribe({
      next: (resp: ILoginResponse) => {
        this.isLoginRequestLoading.set(false);
        this._toastService.success('login successfully', '');
        this._authService.setLoginResponse(resp);
        this._router.navigate(['/analyze']);
      },
      error: (error: IError[]) => {
        if (error[0].errorType == ErrorType.NotFound)
          this.errorMessage.set('email or password is incorrect');
        this.isLoginRequestLoading.set(false);
      },
    });
  }
}
