export interface IBasicPaginatedInput {
  PageSize: number;
  PageIndex: number;
  SearchValue?: string;
}

export interface IPaginatedResponse<T> {
  data: T;
  pagination: IPaginationInfo;
}

export interface IPaginationInfo {
  pageSize: number;
  pageIndex: number;
  totalItems: number;
  totalPages: number;
}
