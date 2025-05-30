export interface ICryptoAnylysisHistoryDto {
  id: string;
  cryptoName: string;
  amount: number;
  holdingDays: number;
  risk: number;
  submitDate: string;
}

export interface IDeleteCryptoAnlysisHistoryRecordsInputDto {
  ids: string[];
}

export interface IAddCryptoAnlysisToCompareInputDto {
  cryptoAnlysisHistoryIds: string[];
}

export interface ICryptoAnlysisCompareItemDto {
  cryptoAnlysisHistoryId: string;
  cryptoName: string;
  investAmount: number;
  holdingDays: number;
  risk: number;
}

export interface IAddCryptoToFavoriteInputDto {
  cryptoId: string;
}

export interface IFavouriteCryptoDto {
  id: string;
  name: string;
  symbol: string;
}
