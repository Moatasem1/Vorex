export interface ICreateAccountInput {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface IVerfiyAccountEmailInput {
  token: string;
}

export interface ILoginInput {
  email: string;
  password: string;
}

export interface ILoginResponse {
  token: string;
  refreshToken: string;
  expiration: Date;
  firstName: string;
  lastName: string;
}
