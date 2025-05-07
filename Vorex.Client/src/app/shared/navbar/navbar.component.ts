import { NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { LucideAngularModule, TrendingUp } from 'lucide-angular';

interface NavbarItem {
  name: string;
  url: string;
}

@Component({
  selector: 'app-navbar',
  imports: [LucideAngularModule, NgClass, RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  // lucide-trending-up
  readonly TrendingUp = TrendingUp;

  navbarItems: NavbarItem[] = [
    { name: 'Home', url: '/home' },
    { name: 'Crypto Anlysis', url: '/crypto-anlysis' },
    { name: 'Products', url: '/products' },
  ];

  //service
  router = inject(Router);

  constructor() {}
}
