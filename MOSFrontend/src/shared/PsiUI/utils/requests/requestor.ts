import { App } from "vue";
import { PsiDictionary } from "@/shared/PsiUI/types/base.ts";

type TMethodType = "GET" | "POST" | "PUT" | "PATCH" | "DELETE" | "HEAD" | "OPTIONS";

type TRequestQuery = PsiDictionary<string | number | boolean | null | Array<any>>;
type TRequestBody = PsiDictionary<any> | Array<any> | string | Blob | FormData | null;

export type TMiddlewareRequest = (request: IPreparedRequest) => Promise<IPreparedRequest>;
export type TMiddlewareResponse = (response: IResponseData) => Promise<IResponseData>;

export type TRequestHeaders = PsiDictionary<string>;

export interface IRequestOptions {
  baseUrl: string;
  headers?: TRequestHeaders;
  middleware?: {
    request?: TMiddlewareRequest[];
    response?: TMiddlewareResponse[];
  };
  handleError?: IRequestErrorHandler;
}

const defaultOptions: IRequestOptions = {
  baseUrl: "",
  headers: {},
  handleError: defaultHandleError
};

export interface IRequestData {
  query?: TRequestQuery;
  body?: TRequestBody;
  headers?: TRequestHeaders;
}

export interface IPreparedRequest {
  url: string;
  options: RequestInit;
}

export interface IResponseData {
  raw: Response;
  data: TRequestBody | null;
  error: Error | null;
}

export type IRequestErrorHandler = (response: IResponseData) => Promise<Error>;

async function defaultHandleError(response: IResponseData): Promise<Error> {
  return new Error(response.raw.statusText);
}

export class PsiRequestor {
  private _headers?: TRequestHeaders;
  private _baseUrl?: string;

  private _middlewaresRequests: TMiddlewareRequest[] = [];
  private _middlewaresResponses: TMiddlewareResponse[] = [];

  private get _hasMiddlewaresRequest(): boolean {
    return this._middlewaresRequests.length > 0;
  }

  private get _hasMiddlewaresResponse(): boolean {
    return this._middlewaresResponses.length > 0;
  }

  private _handleError: IRequestErrorHandler = defaultHandleError;

  constructor(options?: IRequestOptions) {
    this.setOptions(options ?? defaultOptions);
  }

  public setOptions(options: IRequestOptions) {
    this._baseUrl = options?.baseUrl;
    this._headers = options?.headers ?? {};

    this._middlewaresRequests = options?.middleware?.request ?? [];
    this._middlewaresResponses = options?.middleware?.response ?? [];

    if (options.handleError) {
      this._handleError = options.handleError;
    }
  }

  public async get(
    path: string,
    query?: TRequestQuery
  ): Promise<TRequestBody> {
    try {
      return await this.request(
        "GET",
        path, {
          query
        }
      );
    }
    catch (error) {
      throw error;
    }
  }

  public async head(
    path: string,
    query?: TRequestQuery
  ): Promise<TRequestBody> {
    try {
      return await this.request(
        "HEAD",
        path, {
          query
        }
      );
    }
    catch (error) {
      throw error;
    }
  }

  public async post(
    path: string,
    body: TRequestBody
  ): Promise<TRequestBody> {
    try {
      return await this.request(
        "POST",
        path, {
          body
        }
      );
    }
    catch (error) {
      throw error;
    }
  }

  public async put(
    path: string,
    body?: TRequestBody
  ): Promise<TRequestBody> {
    try {
      return await this.request(
        "PUT",
        path, {
          body
        }
      );
    }
    catch (error) {
      throw error;
    }
  }

  public async delete(
    path: string,
    query?: TRequestQuery
  ): Promise<TRequestBody> {
    try {
      return await this.request(
        "DELETE",
        path, {
          query
        }
      );
    }
    catch (error) {
      throw error;
    }
  }

  public async patch(
    path: string,
    body?: TRequestBody
  ): Promise<TRequestBody> {
    try {
      return await this.request(
        "PATCH",
        path, {
          body
        }
      );
    }
    catch (error) {
      throw error;
    }
  }

  private async request(method: TMethodType, path: string, data?: IRequestData): Promise<TRequestBody> {
    try {
      const request = this.prepareRequest(method, path, data);
      const response = await this.sendRequest(request);
      return response;
    }
    catch (error) {
      throw error;
    }
  }

  private prepareRequest(method: TMethodType, path: string, data?: IRequestData) {
    let request: IPreparedRequest = {
      url: this.prepareUrl(path, data?.query),
      options: {
        method: method
      }
    };

    const preparedBody = data?.body ? this.prepareBody(data.body) : undefined;
    const preparedHeaders = data?.headers ? { ...this._headers, ...data.headers } : this._headers;
    if (preparedBody?.isJson && preparedHeaders) {
      preparedHeaders["Content-Type"] = "application/json";
    }

    request.options.body = preparedBody?.body;
    request.options.headers = preparedHeaders;

    if (this._hasMiddlewaresRequest) {
      this._middlewaresRequests.forEach(async (middlewareFn) => {
        request = await middlewareFn(request);
      });
    }

    return request;
  }

  private prepareUrl(path: string, query?: TRequestQuery): string {
    const baseUrl = this._baseUrl;
    if (!baseUrl) {
      throw Error("baseUrl is null");
    }

    const totalUrl = baseUrl + path;

    const preparedQuery = this.convertQuery(query);
    if (preparedQuery) {
      return totalUrl + `?${preparedQuery}`;
    }

    return totalUrl;
  }

  private prepareBody(body: TRequestBody): { isJson: boolean; body: BodyInit } {
    const isJson = !(body instanceof FormData);
    return {
      isJson: isJson,
      body: isJson ? JSON.stringify(body) : body
    };
  }

  private async prepareResponse(raw: Response): Promise<IResponseData> {
    let response: IResponseData = {
      raw: raw,
      data: null,
      error: null
    };

    if (raw.ok) {
      const contentType = raw.headers.get("Content-Type");
      if (contentType != null) {
        response.data = await this.getDataByContentType(contentType, raw);
      }
    }
    else {
      response.error = await this._handleError(response);
    }

    if (this._hasMiddlewaresResponse) {
      this._middlewaresResponses.forEach(async (middlewareFn) => {
        response = await middlewareFn(response);
      });
    }

    return response;
  }

  private async getDataByContentType(contentType: string, raw: Response) {
    if (contentType.indexOf("application/json") >= 0) {
      return await raw.json();
    }
    else {
      return await raw.blob();
    }
  }

  private convertQuery(query?: TRequestQuery): string | null {
    if (!query) {
      return null;
    }

    const params = new URLSearchParams();

    function checkAndSetParam(key: string, value: any): void {
      if (typeof value === "number" || typeof value === "boolean") {
        params.set(key, value.toString());
      }
      else if (typeof value === "string") {
        params.set(key, value);
      }
      else if (value === null) {
        params.set(key, "null");
      }
    }

    Object.keys(query).forEach((key) => {
      const item = query[key];
      if (Array.isArray(query)) {
        const arrayKey = `${key}[]`;
        (<Array<any>>item).forEach(item => checkAndSetParam(arrayKey, item));
      }
      else {
        checkAndSetParam(key, item);
      }
    });

    return params.toString();
  }

  private sendRequest(request: IPreparedRequest): Promise<TRequestBody | Response> {
    const promise: Promise<TRequestBody> = new Promise((resolve, reject) => {
      fetch(request.url, request.options)
        .then((rawResponse) => {
          this.prepareResponse(rawResponse).then((response) => {
            if (response.error === null) {
              resolve(response.data);
            }
            else {
              reject(response.error);
            }
          });
        })
        .catch((error) => {
          // TODO: DI
          reject(error);
          throw error;
        });
    });
    return promise;
  }
}

const requestorInstance = new PsiRequestor();

export const PsiRequestorPlugin = {
  install(app: App, options: IRequestOptions): void {
    requestorInstance.setOptions(options);
    app.config.globalProperties.$requestor = requestorInstance;
  }
};

export function createRequestor(options: IRequestOptions): PsiRequestor {
  return new PsiRequestor(options);
}

export function useRequestor(): PsiRequestor {
  return requestorInstance;
}
