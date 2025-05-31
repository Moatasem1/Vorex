export interface ICreateAccountInputDto {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface IVerfiyEmailInputDto {
  token: string;
}

export interface ILoginInputDto {
  email: string;
  password: string;
}

export interface ILoginResponseDto {
  token: string;
  refreshToken: string;
  expiration: Date;
  firstName: string;
  lastName: string;
}

export interface IRefreshTokenInputDto {
  refreshToken: string;
}

export interface IRefreshTokenResponseDto {
  token: string;
  refreshToken: string;
  expiration: Date;
}

export interface ILogoutInputDto {
  refreshToken: string;
}
