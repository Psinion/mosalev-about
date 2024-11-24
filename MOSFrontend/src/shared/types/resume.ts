import { AppLocale } from "@/shared/enums/common.ts";
import { TUser } from "@/shared/types/user.ts";

export type TResume = {
  id: number;
  title: string;
  firstName: string;
  lastName: string;
  email: string;
  salary: number;
  about: string | null;
  currencyType: CurrencyType;
  dateCreate: string | null;
  dateUpdate: string | null;
};

export type TResumeCompact = {
  id: number;
  title: string;
  pinnedToLocale: AppLocale | null;
  dateCreate: string | null;
  userCreate: TUser | null;
  dateUpdate: string | null;
  userUpdate: TUser | null;
};

export enum CurrencyType {
  Ruble = 1,
  Dollar,
  Euro
}
