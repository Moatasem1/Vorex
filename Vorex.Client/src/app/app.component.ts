import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { initFlowbite } from 'flowbite';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { LucideAngularModule } from 'lucide-angular';
import { ToasterComponent } from './shared/components/toaster/toaster.component';
import { AuthService } from './shared/services/auth.service';
@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    NavbarComponent,
    LucideAngularModule,
    ToasterComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'Vorex.Client';

  authService = inject(AuthService);

  ngOnInit() {
    initFlowbite();
  }
}
