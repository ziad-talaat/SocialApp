namespace Application.Common.ResultPattern
{
    public class Result
    {
        public bool IsSuccess { get;  }
        public IReadOnlyList<Error>? Errors { get;  }
        protected Result(bool isSuccess,IReadOnlyList<Error>errors)
        {
            if (isSuccess && errors?.Count > 0)
                throw new ArgumentException("Successful result cannot have errors.");

            if (!isSuccess && errors?.Count == 0)
                throw new ArgumentException("Failed result must have at least one error.");
            IsSuccess = isSuccess;
            Errors = errors;

        }

        public static Result Success() => new Result(true, []);
        public static Result Failure(IReadOnlyList<Error> error) => new Result(false, error);
        public static Result Failure(Error error) => new Result(false, [error]);
    }

    public class Result<T>
    {
        private T? _value;
        public bool IsSuccess { get; }
        public Error? Error { get; }
        public T? Value
        {
            get => IsSuccess ? _value : default;
        }


        private Result(bool isSuccess, T? data,Error? error) 
        {
            if (isSuccess && data is null)
                throw new ArgumentNullException($" Argumant aren't Compataple");
            if (!isSuccess && error is null)
                throw new ArgumentNullException($" Argumant  aren't Compataple");


            _value = data;
            Error = error;
            IsSuccess = isSuccess;

        }

        public static Result<T> Success(T data) => new Result<T>(true, data,null);
        public static Result<T> Failure(Error error) => new Result<T>(false, default, error);
    }
}
