import { LucideIconData } from 'lucide-angular';

export interface IBasicPaginatedInput {
  PageSize: number;
  PageIndex: number;
  SearchValue?: string;
}

export interface IDatePaginatedInput extends IBasicPaginatedInput {
  startDate?: Date;
  endDate?: Date;
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

export enum ToastType {
  Success = 0,
  Error = 1,
  Warning = 2,
  Info = 3,
}

export interface IToast {
  id: string;
  type: ToastType;
  title: string;
  message: string;
}

export interface IError {
  code: string;
  message: string;
  source: string;
  errorType: number;
}

export interface Language {
  name: string;
  code: string;
}

export interface ICoverImage {
  url: string;
  alt: string;
}
