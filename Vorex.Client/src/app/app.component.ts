import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { initFlowbite } from 'flowbite';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { LucideAngularModule } from 'lucide-angular';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, LucideAngularModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'Vorex.Client';

  ngOnInit() {
    initFlowbite();
  }
}
