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

export interface IRefreshTokenInput {
  refreshToken: string;
}

export interface IRefreshTokenResponse {
  token: string;
  refreshToken: string;
  expiration: Date;
}

export interface ILogoutInput {
  refreshToken: string;
}
