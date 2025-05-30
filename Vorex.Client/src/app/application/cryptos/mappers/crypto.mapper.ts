import {
  IAnalyzeRiskInputDto,
  IAnalyzeRiskResultDto,
  ICryptoListItemDto,
  IHistoricalPriceDto as ICryptoHistoricalPriceDto,
} from '../../../infrastructure/dtos/crypto.dto';
import {
  IAnalyzeRiskInput,
  IAnalyzeRiskResult,
  ICryptoHistoricalPrice,
  ICryptoListItem,
} from '../models/crypto.model';

export function mapCryptoListItemDtoToModel(
  crypto: ICryptoListItemDto
): ICryptoListItem {
  return {
    id: crypto.id,
    name: crypto.name,
    symbol: crypto.symbol,
    isFavourite: crypto.isFavourite,
  };
}

export function mapCryptoAnalizeRiskInputToDto(
  input: IAnalyzeRiskInput
): IAnalyzeRiskInputDto {
  return {
    // holdingDays: input.holdingDays,
    investmentAmount: input.investmentAmount,
  };
}

export function mapCryptoAnalizeRiskResultDtoToModel(
  crypto: IAnalyzeRiskResultDto
): IAnalyzeRiskResult {
  return {
    cryptoAnalysisHistoryId: crypto.cryptoAnalysisHistoryId,
    risk: crypto.riskValue * 100,
  };
}

export function mapCryptoHistoricalPriceDtoToModel(
  crypto: ICryptoHistoricalPriceDto
): ICryptoHistoricalPrice {
  return {
    id: crypto.id,
    date: new Date(crypto.date),
    price: crypto.closingPrice,
  };
}
