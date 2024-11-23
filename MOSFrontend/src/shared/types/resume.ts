import { AppLocale } from "@/shared/enums/common.ts";

export type TResume = {
  id: number;
  title: string;
  firstName: string;
  lastName: string;
  email: string;
  salary: number;
  about: string | null;
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
