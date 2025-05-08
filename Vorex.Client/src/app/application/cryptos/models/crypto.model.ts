export interface ICryptoListItem {
  id: string;
  name: string;
  symbol: string;
}

export interface IAnalyzeRiskInput {
  cryptoId: string;
  investmentAmount: number;
  holdingDays: number;
}

export interface IAnalyzeRiskResult {
  cryptoAnalysisHistoryId: string;
  risk: number;
}
