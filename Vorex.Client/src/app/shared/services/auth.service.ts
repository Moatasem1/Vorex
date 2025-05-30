import { Injectable } from '@angular/core';
import { ILoginResponse } from '../../application/auth/models/auth.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private loginResponse?: ILoginResponse;

  constructor() {}

  getLoginResponse(): ILoginResponse | undefined {
    if (!this.loginResponse) {
      this.loginResponse = JSON.parse(
        localStorage.getItem('loginResponse') || ''
      );
    }
    return this.loginResponse;
  }

  setLoginResponse(loginResponse: ILoginResponse) {
    this.loginResponse = loginResponse;
    localStorage.setItem('loginResponse', JSON.stringify(loginResponse));
  }

  clearLoginResponse() {
    this.loginResponse = undefined;
    localStorage.removeItem('loginResponse');
  }

  isAuthenticated(): boolean {
    return !!this.getLoginResponse();
  }
}
