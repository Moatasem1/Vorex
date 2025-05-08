import {
  IAnalyzeRiskInputDto,
  IAnalyzeRiskResultDto,
  ICryptoListItemDto,
} from '../../../infrastructure/dtos/crypto.dto';
import {
  IAnalyzeRiskInput,
  IAnalyzeRiskResult,
  ICryptoListItem,
} from '../models/crypto.model';

export function mapCryptoListItemDtoToModel(
  crypto: ICryptoListItemDto
): ICryptoListItem {
  return {
    id: crypto.id,
    name: crypto.name,
    symbol: crypto.symbol,
  };
}

export function mapCryptoAnalizeRiskInputToDto(
  input: IAnalyzeRiskInput
): IAnalyzeRiskInputDto {
  return {
    holdingDays: input.holdingDays,
    investmentAmount: input.investmentAmount,
  };
}

export function mapCryptoAnalizeRiskResultDtoToModel(
  crypto: IAnalyzeRiskResultDto
): IAnalyzeRiskResult {
  return {
    cryptoAnalysisHistoryId: crypto.cryptoAnalysisHistoryId,
    risk: crypto.riskValue,
  };
}
