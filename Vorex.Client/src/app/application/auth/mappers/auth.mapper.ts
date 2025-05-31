import {
  ICreateAccountInputDto,
  ILoginInputDto,
  ILoginResponseDto,
  ILogoutInputDto,
  IRefreshTokenInputDto,
  IRefreshTokenResponseDto,
  IVerfiyEmailInputDto,
} from '../../../infrastructure/dtos/auth.dto';
import {
  ICreateAccountInput,
  ILoginInput,
  ILoginResponse,
  IRefreshTokenInput,
  IRefreshTokenResponse,
  IVerfiyAccountEmailInput,
} from '../models/auth.model';

export function mapCreateAccountInputModelToDto(input: ICreateAccountInput) {
  return {
    firstName: input.firstName,
    lastName: input.lastName,
    email: input.email,
    password: input.password,
  } as ICreateAccountInputDto;
}

export function mapVerifyEmailInputModelToDto(input: IVerfiyAccountEmailInput) {
  return {
    token: input.token,
  } as IVerfiyEmailInputDto;
}

export function mapLoginInputModelToDto(input: ILoginInput) {
  return {
    email: input.email,
    password: input.password,
  } as ILoginInputDto;
}

export function mapLoginResponseDtoToModel(input: ILoginResponseDto) {
  return {
    token: input.token,
    refreshToken: input.refreshToken,
    expiration: input.expiration,
    firstName: input.firstName,
    lastName: input.lastName,
  } as ILoginResponse;
}

export function mapRefreshTokenInputModelToDto(input: IRefreshTokenInput) {
  return {
    refreshToken: input.refreshToken,
  } as IRefreshTokenInputDto;
}

export function mapRefreshTokenResponseDtoToModel(
  input: IRefreshTokenResponseDto
) {
  return {
    token: input.token,
    refreshToken: input.refreshToken,
    expiration: input.expiration,
  } as IRefreshTokenResponse;
}

export function mapLogoutInputModelToDto(input: IRefreshTokenInput) {
  return {
    refreshToken: input.refreshToken,
  } as ILogoutInputDto;
}
