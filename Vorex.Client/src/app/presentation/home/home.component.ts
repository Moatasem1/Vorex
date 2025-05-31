import { Component, inject } from '@angular/core';
import { SearchBarComponent } from '../../shared/components/search-bar/search-bar.component';
import { Router, RouterLink } from '@angular/router';
import { heroCoverImages, homeFeatures } from './home.constant';
import {
  Calculator,
  Grape,
  Heart,
  History,
  LucideAngularModule,
  Search,
  TrendingUp,
} from 'lucide-angular';

@Component({
  selector: 'app-home',
  imports: [SearchBarComponent, RouterLink, LucideAngularModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  coverImages = heroCoverImages;
  TrendingUp = TrendingUp;
  currentCoverImageIndex = 0;
  features = homeFeatures;

  // services
  private _router = inject(Router);

  ngOnInit() {
    this.playHeroImages();
  }

  playHeroImages() {
    setInterval(() => {
      this.currentCoverImageIndex =
        (this.currentCoverImageIndex + 1) % this.coverImages.length;
    }, 5000);
  }

  navigateToCryptoAnalysis(searchText: string) {
    this._router.navigate([`/analyze`], {
      queryParams: { search: searchText },
    });
  }
}
