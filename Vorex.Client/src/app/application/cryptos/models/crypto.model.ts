export interface ICryptoListItem {
  id: string;
  name: string;
  symbol: string;
  isFavourite: boolean;
  voltiltlyLevelId: VolatilityLevel;
}

export interface IAnalyzeRiskInput {
  cryptoId: string;
  investmentAmount: number;
  // holdingDays: number;
}

export interface IAnalyzeRiskResult {
  cryptoAnalysisHistoryId: string;
  risk: number;
  returnOfInvestment: number;
}

export interface IGetCryptoHistoricalPricesInput {
  cryptoId: string;
  startDate?: string;
  endDate?: string;
}

export interface ICryptoHistoricalPriceItem {
  id: string;
  date: Date;
  price: number;
}

export interface ICryptoHistoricalPrice {
  minDate: string;
  maxDate: string;
  startDate: string;
  endDate: string;
  data: ICryptoHistoricalPriceItem[];
}

export enum VolatilityLevel {
  Low = 0,
  Medium,
  High,
}
