import { VolatilityLevel } from '../../application/cryptos/models/crypto.model';

export interface ICryptoListItemDto {
  id: string;
  name: string;
  symbol: string;
  isFavourite: boolean;
  voltiltlyLevelId: VolatilityLevel;
}

export interface IAnalyzeRiskInputDto {
  investmentAmount: number;
  // holdingDays: number;
}

export interface IAnalyzeRiskResultDto {
  cryptoAnalysisHistoryId: string;
  riskValue: number;
  returnOfInvestment: number;
}

export interface IHistoricalPriceItemDto {
  id: string;
  date: string;
  closingPrice: number;
}

export interface ICryptoHistoricalPriceDto {
  minDate: string;
  maxDate: string;
  startDate: string;
  endDate: string;
  data: IHistoricalPriceItemDto[];
}
