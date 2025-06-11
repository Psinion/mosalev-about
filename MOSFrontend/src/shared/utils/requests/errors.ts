export type TServerError = {
  statusCode: number;
  code: string;
  description: string;
  errorType: number;
};

export class ServerError extends Error {
  header: string;
  statusCode: number;
  code: string;
  errorType: number;

  constructor(statusCode: number, error?: TServerError) {
    super(error?.description ?? "");
    this.statusCode = statusCode;
    this.code = error?.code ?? "";
    this.errorType = error?.errorType ?? -1;
  }
}
