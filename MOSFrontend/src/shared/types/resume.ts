export type TResume = {
  id: number;
  title: string;
  email: string;
  salary: number;
  currencyType: CurrencyType;
};

export enum CurrencyType {
  Ruble = 1,
  Dollar,
  Euro
}
