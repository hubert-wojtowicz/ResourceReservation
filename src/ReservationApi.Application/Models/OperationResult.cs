namespace ReservationApi.Application.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public ErrorModel? Error { get; }

        protected OperationResult(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        protected OperationResult(ErrorModel error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        public static OperationResult<T> Success(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Success value cannot be null.");
            }

            return new OperationResult<T>(value);
        }

        public static OperationResult<T> Failure(ErrorModel error)
        {
            if (string.IsNullOrEmpty(error.Message))
            {
                throw new ArgumentNullException(nameof(error), "Error message cannot be null or empty.");
            }

            return new OperationResult<T>(error);
        }

        public static implicit operator OperationResult<T>(T value)
        {
            return Success(value);
        }

        public static implicit operator OperationResult<T>(ErrorModel error)
        {
            return Failure(error);
        }
    }
}
