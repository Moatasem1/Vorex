import { NgClass } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import {
  ActivatedRoute,
  NavigationEnd,
  Router,
  RouterLink,
  RouterLinkActive,
} from '@angular/router';
import {
  ChevronDown,
  CircleUser,
  Languages,
  LogOut,
  LucideAngularModule,
  TrendingUp,
} from 'lucide-angular';
import { AuthService } from '../services/auth.service';
import { Language } from '../types/shared.types';
import { LogOutUseCase } from '../../application/auth/usecases/logout.usecase';
import { ILogoutInput } from '../../application/auth/models/auth.model';
import { filter, map } from 'rxjs';

interface NavbarItem {
  name: string;
  url: string;
  hideIfUnAuthenticated?: boolean;
}

@Component({
  selector: 'app-navbar',
  imports: [LucideAngularModule, RouterLinkActive, NgClass, RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  // lucide-trending-up
  readonly TrendingUp = TrendingUp;
  readonly Languages = Languages;
  readonly LogOut = LogOut;
  readonly ChevronDown = ChevronDown;

  readonly LanguagesList: Language[] = [
    { name: 'English', code: 'en' },
    // { name: 'Arabic', code: 'ar' },
  ];

  navbarItems: NavbarItem[] = [
    { name: 'Home', url: '/home', hideIfUnAuthenticated: false },
    { name: 'analyze', url: '/analyze', hideIfUnAuthenticated: false },
    { name: 'history', url: '/history', hideIfUnAuthenticated: true },
    { name: 'compare', url: '/compare', hideIfUnAuthenticated: true },
    { name: 'favorites', url: '/favorites', hideIfUnAuthenticated: true },
  ];

  showNavbar = signal<boolean>(true);

  //service
  router = inject(Router);
  authService = inject(AuthService);
  private _logoutUseCase = inject(LogOutUseCase);
  private _activatedRoute = inject(ActivatedRoute);

  constructor() {
    this.hideNavbarIfRouteSayHide();
  }

  hideNavbarIfRouteSayHide() {
    this.router.events
      .pipe(
        filter(
          (event): event is NavigationEnd => event instanceof NavigationEnd
        ),
        map(
          () =>
            this._activatedRoute.firstChild?.snapshot.data['hideNavbar'] ||
            false
        )
      )
      .subscribe((hideNavbar: boolean) => {
        this.showNavbar.set(!hideNavbar);
      });
  }

  logout() {
    const input = {
      refreshToken: this.authService.getLoginResponse()?.refreshToken,
    } as ILogoutInput;
    this._logoutUseCase.execute(input).subscribe({
      next: () => {
        this.authService.clearLoginResponse();
        this.router.navigate(['/login']);
      },
      error: () => {
        this.authService.clearLoginResponse();
        this.router.navigate(['/login']);
      },
    });
  }
}
