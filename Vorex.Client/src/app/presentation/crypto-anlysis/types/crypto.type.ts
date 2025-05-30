import {
  IAnalyzeRiskInput,
  IAnalyzeRiskResult,
  ICryptoHistoricalPrice,
} from '../../../application/cryptos/models/crypto.model';

export interface IAnaylizeRiskResultModalInput
  extends IAnalyzeRiskInput,
    IAnalyzeRiskResult {
  cryptoName: string;
}

export interface ICryptoHistoricalPriceModalInput {
  cryptoId: string;
  cryptoName: string;
}

type CryptoAnalysisHistoryColumn =
  | 'cryptoName'
  | 'investmentAmount'
  | 'holdingDays'
  | 'risk'
  | 'submitDate';

export interface ICryptoHistoryTableColumn {
  id: number;
  key: CryptoAnalysisHistoryColumn;
  name: string;
  isAsc: boolean;
}
