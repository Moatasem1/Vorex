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
}

export interface IGetCryptoHistoricalPricesInput {
  cryptoId: string;
  startDate?: Date;
  endDate?: Date;
}

export interface ICryptoHistoricalPrice {
  id: string;
  date: Date;
  price: number;
}

export enum VolatilityLevel {
  Low = 0,
  Medium,
  High,
}
