import {
  IAddCryptoAnlysisToCompareInputDto,
  IAddCryptoToFavoriteInputDto,
  ICryptoAnlysisCompareItemDto,
  ICryptoAnylysisHistoryDto,
  IDeleteCryptoAnlysisHistoryRecordsInputDto,
  IFavouriteCryptoDto,
} from '../../../infrastructure/dtos/user.dto';
import {
  IAddCryptoAnlysisToCompareInput,
  IAddCryptoToFavoriteInput,
  ICryptoAnylysisHistory,
  IDeleteCryptoAnlysisHistoryRecordsInput,
  IFavouriteCrypto,
} from '../../users/models/user.model';

export function mapCryptoAnylysisHistoryDtoToModel(
  record: ICryptoAnylysisHistoryDto
): ICryptoAnylysisHistory {
  return {
    id: record.id,
    cryptoName: record.cryptoName,
    investmentAmount: record.amount,
    holdingDays: record.holdingDays,
    risk: record.risk,
    submitDate: new Date(record.submitDate),
    isChecked: false,
  };
}

export function mapDeleteCryptoAnlysisHistoryRecordsInputModelToDto(
  input: IDeleteCryptoAnlysisHistoryRecordsInput
): IDeleteCryptoAnlysisHistoryRecordsInputDto {
  return {
    ids: input.ids,
  };
}

export function mapAddCryptoAnlysisToCompareInputModelToDto(
  record: IAddCryptoAnlysisToCompareInput
): IAddCryptoAnlysisToCompareInputDto {
  return {
    cryptoAnlysisHistoryIds: record.cryptoAnlysisHistoryIds,
  };
}

export function mapCryptoAnylysisHistoryCompareItemDtoToModel(
  input: ICryptoAnlysisCompareItemDto
): ICryptoAnlysisCompareItemDto {
  return {
    cryptoAnlysisHistoryId: input.cryptoAnlysisHistoryId,
    cryptoName: input.cryptoName,
    investAmount: input.investAmount,
    holdingDays: input.holdingDays,
    risk: input.risk,
  };
}

export function mapAddCryptoToFavoriteInputModelToDto(
  input: IAddCryptoToFavoriteInput
): IAddCryptoToFavoriteInputDto {
  return {
    cryptoId: input.cryptoId,
  };
}

export function mapCryptoFavouriteDtoToModel(
  input: IFavouriteCryptoDto
): IFavouriteCrypto {
  return {
    id: input.id,
    name: input.name,
    symbol: input.symbol,
  };
}
