namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Results;

public abstract class Result<T>
{
    public bool IsSuccess { get; protected set; }

    public bool IsFailure => !IsSuccess;

    private T? _value;
    private string? _errorMessage;

    public T Value
    {
        get
        {
            if (IsSuccess && _value is not null)
                return _value;
            throw new InvalidOperationException("Result has not been successful.");
        }

        protected set
        {
            _value = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            if (IsFailure && _errorMessage is not null)
                return _errorMessage;
            throw new InvalidOperationException("Result has not been successful.");
        }

        protected set
        {
            _errorMessage = value;
        }
    }

    public class Success : Result<T>
    {
        public Success(T value)
        {
            IsSuccess = true;
            Value = value;
        }
    }

    public class Failure : Result<T>
    {
        public Failure(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}