import { Injectable } from '@angular/core';
import { ILoginResponse } from '../../application/auth/models/auth.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private loginResponse?: ILoginResponse;
  private preferLanguageCode = 'en';

  constructor() {}

  getLoginResponse(): ILoginResponse | undefined {
    if (!this.loginResponse) {
      const stored = localStorage.getItem('loginResponse');
      if (stored) {
        this.loginResponse = JSON.parse(stored);
      }
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

  getFullName() {
    const loginResponse = this.getLoginResponse();
    return loginResponse?.firstName + ' ' + loginResponse?.lastName;
  }

  setPreferLanguageCode(code: string) {
    this.preferLanguageCode = code;
    localStorage.setItem(
      'preferLanguageCode',
      JSON.stringify(this.preferLanguageCode)
    );
  }

  getPreferLanguageCode() {
    if (this.preferLanguageCode == 'en') {
      const stored = localStorage.getItem('preferLanguageCode');
      this.preferLanguageCode = stored ? JSON.parse(stored) : 'en';
    }
    return this.preferLanguageCode;
  }
}
