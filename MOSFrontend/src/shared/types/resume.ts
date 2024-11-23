import { AppLocale } from "@/shared/enums/common.ts";

export type TResume = {
  id: number;
  title: string;
  email: string;
  salary: number;
  currencyType: CurrencyType;
};

export type TResumeCompact = {
  id: number;
  title: string;
  pinnedToLocale: AppLocale | null;
};

export enum CurrencyType {
  Ruble = 1,
  Dollar,
  Euro
}
