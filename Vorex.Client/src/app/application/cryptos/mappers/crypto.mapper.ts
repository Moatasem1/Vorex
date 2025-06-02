import {
  IAnalyzeRiskInputDto,
  IAnalyzeRiskResultDto,
  ICryptoHistoricalPriceDto,
  ICryptoListItemDto,
} from '../../../infrastructure/dtos/crypto.dto';
import {
  IAnalyzeRiskInput,
  IAnalyzeRiskResult,
  ICryptoHistoricalPrice,
  ICryptoHistoricalPriceItem,
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
    voltiltlyLevelId: crypto.voltiltlyLevelId,
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
    returnOfInvestment: crypto.returnOfInvestment,
  };
}

export function mapCryptoHistoricalPriceDtoToModel(
  crypto: ICryptoHistoricalPriceDto
): ICryptoHistoricalPrice {
  return {
    maxDate: crypto.maxDate,
    minDate: crypto.minDate,
    startDate: crypto.startDate,
    endDate: crypto.endDate,
    data: crypto.data.map(
      (item) =>
        ({
          id: item.id,
          date: new Date(item.date),
          price: item.closingPrice,
        } as ICryptoHistoricalPriceItem)
    ),
  };
}
