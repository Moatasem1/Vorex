import { CommonModule, JsonPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { LucideAngularModule, TrendingUp } from 'lucide-angular';
import { authcoverImages } from '../auth.constant';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ICreateAccountForm } from '../auth.types';
import { CreateAccountUseCase } from '../../../application/auth/usecases/create-account.usecase';
import { ICreateAccountInput } from '../../../application/auth/models/auth.model';
import { ToastrService } from '../../../shared/services/toastr.service';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';
import { IError } from '../../../shared/types/shared.types';
import { ErrorType } from '../../../shared/constants/shared.constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  imports: [
    LucideAngularModule,
    CommonModule,
    ReactiveFormsModule,
    JsonPipe,
    LoaderComponent,
  ],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
})
export class SignupComponent {
  // icons
  readonly TrendingUp = TrendingUp;
  coverImages = authcoverImages;
  currentCoverImageIndex = 0;

  createAccountForm!: FormGroup<ICreateAccountForm>;
  isCreateAccountRequestLoading = signal(false);
  errorMessage = signal('');
  // services
  private _formBuilderService = inject(FormBuilder);
  private _createAccountUseCase = inject(CreateAccountUseCase);
  private _toastService = inject(ToastrService);
  private _router = inject(Router);

  constructor() {}

  ngOnInit() {
    this.initializeCreateAccountForm();
    this.playImages();
  }

  playImages() {
    setInterval(() => {
      this.currentCoverImageIndex =
        (this.currentCoverImageIndex + 1) % this.coverImages.length;
    }, 5000);
  }

  initializeCreateAccountForm() {
    this.createAccountForm = this._formBuilderService.nonNullable.group({
      firstName: [
        '',
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(50),
        ],
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(50),
        ],
      ],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(20),
        ],
      ],
      confirmPassword: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(20),
        ],
      ],
    });
  }

  creatAccount() {
    const input = {
      firstName: this.createAccountForm.value.firstName,
      lastName: this.createAccountForm.value.lastName,
      email: this.createAccountForm.value.email,
      password: this.createAccountForm.value.password,
    } as ICreateAccountInput;

    this.isCreateAccountRequestLoading.set(true);
    this.errorMessage.set('');
    this._createAccountUseCase.execute(input).subscribe({
      next: () => {
        this.isCreateAccountRequestLoading.set(false);
        this._toastService.success(
          'account created successfully',
          'please check your email to confirm your account'
        );
        this._router.navigate(['/auth/login']);
      },
      error: (error: IError[]) => {
        if (error[0].errorType == ErrorType.Conflict)
          this.errorMessage.set(
            'email already exists, if you have an account please login'
          );
        this.isCreateAccountRequestLoading.set(false);
      },
    });
  }
}
