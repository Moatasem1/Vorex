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
import {
  ICryptoHistoricalPrice,
  ICryptoHistoricalPriceItem,
  IGetCryptoHistoricalPricesInput,
} from '../../../application/cryptos/models/crypto.model';
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
import { FormsModule } from '@angular/forms';

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
  imports: [PopupComponent, FormsModule, LoaderComponent],
  templateUrl: './crypto-historical-prices-model.component.html',
  styleUrl: './crypto-historical-prices-model.component.scss',
})
export class CryptoHistoricalPricesModelComponent implements OnChanges {
  @ViewChild(PopupComponent) model!: PopupComponent;
  crypto = input.required<ICryptoHistoricalPriceModalInput>();
  cryptoHistoricalPrices = signal<ICryptoHistoricalPrice | null>(null);
  @ViewChild('chart') historicalPricesChartElement!: ElementRef;
  histotricalPricesChart!: Chart;
  modelClosed = output<void>();
  isCryptoHistoricalPricesLoading = signal(false);
  isCryptoHistoricalPricesUpdateLoading = signal(false);
  startDate = '';
  endDate = '';
  maxDate = '';
  minDate = '';

  //services
  GetCryptoHistoricalPricesUseCase = inject(GetCryptoHistoricalPricesUseCase);
  constructor() {}
  async ngOnChanges(changes: SimpleChanges) {
    if (changes['crypto'] && changes['crypto'].currentValue) {
      console.log('i am inside historical prices modal');
      this.isCryptoHistoricalPricesLoading.set(true);
      await this.fetchCryptoHistoricalPrices();
      this.isCryptoHistoricalPricesLoading.set(false);

      setTimeout(() => {
        this.drawChart();
      });
    }
  }

  fetchCryptoHistoricalPrices() {
    const input = {
      startDate: this.startDate,
      endDate: this.endDate,
      cryptoId: this.crypto().cryptoId,
    } as IGetCryptoHistoricalPricesInput;
    return new Promise((resolve, reject) => {
      this.GetCryptoHistoricalPricesUseCase.execute(input).subscribe({
        next: (resp) => {
          this.cryptoHistoricalPrices.set(resp);
          this.startDate = this.cryptoHistoricalPrices()!.startDate;
          this.endDate = this.cryptoHistoricalPrices()!.endDate;
          this.maxDate = this.cryptoHistoricalPrices()!.maxDate;
          this.minDate = this.cryptoHistoricalPrices()!.minDate;
          resolve(resp);
        },
        error: () => {
          this.model.hide();
          reject();
        },
      });
    });
  }

  async updateGraph() {
    this.isCryptoHistoricalPricesUpdateLoading.set(true);
    await this.fetchCryptoHistoricalPrices();
    this.isCryptoHistoricalPricesUpdateLoading.set(false);
    setTimeout(() => {
      this.drawChart();
    });
  }

  toDateOnly(date: Date) {
    return date.toISOString().split('T')[0];
  }

  drawChart() {
    if (this.histotricalPricesChart) this.histotricalPricesChart.destroy();
    if (this.cryptoHistoricalPrices() === null) return;
    this.histotricalPricesChart = new Chart(
      this.historicalPricesChartElement.nativeElement,
      {
        type: 'line',
        data: {
          labels: this.cryptoHistoricalPrices()!.data.map((item) =>
            item.date.toLocaleDateString('en-GB')
          ),
          datasets: [
            {
              label: 'Closing Price',
              data: this.cryptoHistoricalPrices()!.data.map(
                (item) => item.price
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
          elements: {
            point: {
              radius: this.cryptoHistoricalPrices()!.data.length > 60 ? 0 : 3, // Small points for cleaner look
              hoverRadius: 6,
              backgroundColor: '#ad46ff',
              borderColor: '#ad46ff',
            },
            line: {
              borderWidth: 2,
            },
          },
        },
      }
    );
  }

  emitModelClosed() {
    this.modelClosed.emit();
    this.startDate = '';
    this.endDate = '';
  }
}
