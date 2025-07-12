import type { Ref } from "vue";

export enum TResultErrorType {
  None,
  Multiple,
  Failure,
  NotFound,
  Forbidden
}

interface IOperationError {
  errorType: TResultErrorType;
  errorCode?: number | string;
  message: string;
  errors?: IOperationError[];
}

export type OperationResult<T> = SuccessResult<T> | FailureResult<T>;

abstract class BaseResult<T> {
  abstract readonly success: boolean;
  abstract readonly error?: IOperationError;

  abstract match(success: (value: T) => void, failure: (error: IOperationError) => void): this;
}

class SuccessResult<T> extends BaseResult<T> {
  readonly success = true;
  readonly error = undefined;

  constructor(public readonly value: T) {
    super();
  }

  match(success: (value: T) => void, failure?: (error: IOperationError) => void): this {
    if (success !== null) {
      success(this.value);
    }
    return this;
  }
}

class FailureResult<T> extends BaseResult<T> {
  readonly success = false;

  constructor(public readonly error: IOperationError) {
    super();
  }

  match(success: (value: T) => void, failure?: (error: IOperationError) => void): this {
    if (failure) {
      failure(this.error);
    }
    return this;
  }
}

export abstract class Result {
  public static success(): SuccessResult<void>;
  public static success<T>(value: T): SuccessResult<T>;
  public static success<T = void>(value?: T): SuccessResult<T> {
    return new SuccessResult(value as T);
  }

  public static failure(message: string, errorCode?: number | string): FailureResult<any> {
    return new FailureResult({
      errorType: TResultErrorType.Failure,
      errorCode: errorCode,
      message: message
    });
  }

  public static forbidden(message: string, errorCode?: number | string): FailureResult<any> {
    return new FailureResult({
      errorType: TResultErrorType.Forbidden,
      errorCode: errorCode,
      message: message
    });
  }

  public static notFound(message: string, errorCode?: number | string): FailureResult<any> {
    return new FailureResult({
      errorType: TResultErrorType.NotFound,
      errorCode: errorCode,
      message: message
    });
  }

  public static async withLoading<T>(
    action: () => Promise<OperationResult<T>>,
    loading: Ref<boolean>,
    handlers?: {
      success?: (value: T) => void;
      failure?: (error: IOperationError) => void;
    }
  ): Promise<OperationResult<T>> {
    loading.value = true;
    try {
      const result = await action();

      result.match(
        value => handlers?.success?.(value),
        error => handlers?.failure?.(error)
      );

      return result;
    }
    catch (e: any) {
      const error = Result.unexpectedError(e);
      handlers?.failure?.(error);
      return new FailureResult(error);
    }
    finally {
      loading.value = false;
    }
  }

  public static async all<T extends any[]>(
    promises: [...{ [K in keyof T]: Promise<OperationResult<T[K]>> }]
  ): Promise<OperationResult<T>> {
    try {
      const results = await Promise.all(promises);

      const errors = results.filter(r => !r.success).map(r => r.error);

      if (errors.length > 0) {
        return new FailureResult({
          errorType: TResultErrorType.Multiple,
          message: "Multiple errors occurred",
          errors: errors
        }) as OperationResult<T>;
      }

      const values = results.map(r => (r as SuccessResult<any>).value) as T;

      return Result.success(values) as OperationResult<T>;
    }
    catch (e) {
      return Result.failure(e instanceof Error ? e.message : e);
    }
  }

  private static unexpectedError(e: unknown): IOperationError {
    return {
      errorType: TResultErrorType.Failure,
      message: e instanceof Error ? e.message : "Unknown error"
    };
  }
}
