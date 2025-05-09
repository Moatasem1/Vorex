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
