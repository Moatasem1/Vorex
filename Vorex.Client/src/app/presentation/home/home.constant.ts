import { ICoverImage } from '../../shared/types/shared.types';
import { HomeFeature } from './home.types';
import {
  Calculator,
  Grape,
  Heart,
  History,
  Search,
  ChartLine,
} from 'lucide-angular';

export const heroCoverImages: ICoverImage[] = [
  {
    url: './images/home/hero-1.jpg',
    alt: 'hero-1',
  },
  {
    url: './images/home/hero-2.jpg',
    alt: 'hero-2',
  },
  {
    url: './images/home/hero-3.jpg',
    alt: 'hero-3',
  },
];

export const homeFeatures: HomeFeature[] = [
  {
    icon: Search,
    title: 'Smart Crypto Search',
    description:
      'Find any cryptocurrency instantly with our powerful search engine.',
  },
  {
    icon: ChartLine,
    title: 'Historical Data Prices',
    description:
      'Deep dive into price history and market trends with interactive charts.',
  },
  {
    icon: Calculator,
    title: 'ROI Calculator',
    description:
      'Calculate returns, analyze volatility, and optimize your investment strategy',
  },
  {
    icon: Heart,
    title: 'Favorites Portfolio',
    description: 'Save and track your favorite cryptocurrencies in one place',
  },
  {
    icon: History,
    title: 'Analysis History',
    description:
      'Keep records of your analysis and compare different investments',
  },
];
