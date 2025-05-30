import { FormControl } from '@angular/forms';

export interface IAuthCoverImage {
  url: string;
  alt: string;
}

export interface ICreateAccountForm {
  firstName: FormControl<string>;
  lastName: FormControl<string>;
  email: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
}

export interface ILoginForm {
  email: FormControl<string>;
  password: FormControl<string>;
}
