export interface ICryptoAnylysisHistory {
  id: string;
  cryptoName: string;
  investmentAmount: number;
  holdingDays: number;
  risk: number;
  submitDate: Date;
  isChecked: boolean;
}

export interface IDeleteCryptoAnlysisHistoryRecordsInput {
  ids: string[];
}

export interface IAddCryptoAnlysisToCompareInput {
  cryptoAnlysisHistoryIds: string[];
}

export interface ICryptoAnlysisCompareItem {
  cryptoAnlysisHistoryId: string;
  cryptoName: string;
  investAmount: number;
  holdingDays: number;
  risk: number;
}
export interface IAddCryptoToFavoriteInput {
  cryptoId: string;
}

export interface IFavouriteCrypto {
  id: string;
  name: string;
  symbol: string;
}
