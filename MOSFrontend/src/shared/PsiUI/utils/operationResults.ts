import type { Ref } from "vue";

export enum ErrorType {
  None,
  Failure,
  NotFound
}

interface IOperationError {
  errorType: ErrorType;
  message: string;
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

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  match(success: (value: T) => void, failure: (error: IOperationError) => void): this {
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

  public static failure(message: string): FailureResult<never> {
    return new FailureResult({
      errorType: ErrorType.Failure,
      message: message
    });
  }

  public static notFound(message: string): FailureResult<never> {
    return new FailureResult({
      errorType: ErrorType.NotFound,
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
    catch (e) {
      const error = Result.unexpectedError(e);
      handlers?.failure?.(error);
      return new FailureResult(error);
    }
    finally {
      loading.value = false;
    }
  }

  private static unexpectedError(e: unknown): IOperationError {
    return {
      errorType: ErrorType.Failure,
      message: e instanceof Error ? e.message : "Unknown error"
    };
  }
}
