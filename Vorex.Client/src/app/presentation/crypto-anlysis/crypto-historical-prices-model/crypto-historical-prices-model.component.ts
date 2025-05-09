import {
  Component,
  ElementRef,
  inject,
  input,
  OnChanges,
  output,
  signal,
  SimpleChange,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { PopupComponent } from '../../../shared/components/popup/popup.component';
import { ICryptoHistoricalPriceModalInput } from '../types/crypto.type';
import { GetCryptoHistoricalPricesUseCase } from '../../../application/cryptos/use-cases/get-historical-prices.usecase';
import { ICryptoHistoricalPrice } from '../../../application/cryptos/models/crypto.model';
import {
  CategoryScale,
  Chart,
  Filler,
  Legend,
  LinearScale,
  LineController,
  LineElement,
  PointElement,
  Title,
  Tooltip,
} from 'chart.js';
import { EmptyResultComponent } from '../../../shared/components/empty-result/empty-result.component';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';

Chart.register(
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  CategoryScale,
  Tooltip,
  Legend,
  Filler
);

@Component({
  selector: 'app-crypto-historical-prices-model',
  imports: [PopupComponent],
  templateUrl: './crypto-historical-prices-model.component.html',
  styleUrl: './crypto-historical-prices-model.component.scss',
})
export class CryptoHistoricalPricesModelComponent implements OnChanges {
  @ViewChild(PopupComponent) model!: PopupComponent;
  crypto = input.required<ICryptoHistoricalPriceModalInput>();
  cryptoHistoricalPrices = signal<ICryptoHistoricalPrice[]>([]);
  @ViewChild('chart') historicalPricesChartElement!: ElementRef;
  histotricalPricesChart!: Chart;
  modelClosed = output<void>();
  isCryptoHistoricalPricesLoading = signal(false);

  //services
  GetCryptoHistoricalPricesUseCase = inject(GetCryptoHistoricalPricesUseCase);
  constructor() {}
  async ngOnChanges(changes: SimpleChanges) {
    if (changes['crypto'] && changes['crypto'].currentValue) {
      console.log('i am inside historical prices modal');
      await this.fetchCryptoHistoricalPrices();
      setTimeout(() => {
        this.drawChart();
      });
    }
  }

  fetchCryptoHistoricalPrices() {
    this.isCryptoHistoricalPricesLoading.set(true);
    return new Promise((resolve, reject) => {
      this.GetCryptoHistoricalPricesUseCase.execute({
        cryptoId: this.crypto().cryptoId,
      }).subscribe({
        next: (resp) => {
          this.cryptoHistoricalPrices.set(resp);
          this.isCryptoHistoricalPricesLoading.set(false);
          resolve(resp);
        },
        error: () => {
          this.isCryptoHistoricalPricesLoading.set(false);
          this.model.hide();
          reject();
        },
      });
    });
  }

  drawChart() {
    let sortedCryptoHistoricalPrices = this.cryptoHistoricalPrices().sort(
      (a, b) => a.date.getDate() - b.date.getDate()
    );
    this.histotricalPricesChart = new Chart(
      this.historicalPricesChartElement.nativeElement,
      {
        type: 'line',
        data: {
          labels: sortedCryptoHistoricalPrices.map(
            (item) =>
              item.date.toLocaleString('default', { month: 'short' }) +
              ', ' +
              item.date.getFullYear()
          ),
          datasets: [
            {
              label: 'Closing Price',
              data: sortedCryptoHistoricalPrices.map(
                (item) => item.price * 1000000
              ),
              borderColor: '#ad46ff',
              backgroundColor: 'white',
              fill: false,
              tension: 0.1,
            },
          ],
        },
        options: {
          responsive: true,
          plugins: {
            title: {
              display: true,
              text: `${this.crypto().cryptoName} Closing Price Over Years`,
            },
          },
          scales: {
            x: {
              title: {
                display: true,
                text: 'Year',
              },
            },
            y: {
              title: {
                display: true,
                text: 'Closing Price (USD)',
              },
            },
          },
        },
      }
    );
  }

  emitModelClosed() {
    this.modelClosed.emit();
  }
}
