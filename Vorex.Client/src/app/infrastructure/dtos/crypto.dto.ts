export interface ICryptoListItemDto {
  id: string;
  name: string;
  symbol: string;
}

export interface IAnalyzeRiskInputDto {
  investmentAmount: number;
  holdingDays: number;
}

export interface IAnalyzeRiskResultDto {
  cryptoAnalysisHistoryId: string;
  riskValue: number;
}
