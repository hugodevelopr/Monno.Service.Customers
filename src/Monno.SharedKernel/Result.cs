namespace Monno.SharedKernel;

public class Result
{
    public bool IsSuccess { get; }
    public IList<Error> Errors { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, IList<Error> errors)
    {
        if (isSuccess && errors.Any(x => x.Message != string.Empty))
            throw new InvalidOperationException();

        if (!isSuccess && errors.Any(x => x.Message == string.Empty))
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Fail(IList<Error> errors)
    {
        return new Result(false, errors);
    }

    public static Result<T?> Fail<T>(IList<Error> errors)
    {
        return new Result<T?>(default, false, errors);
    }

    public static Result Fail(Error error)
    {
        return new Result(false, new List<Error> { error });
    }

    public static Result<T?> Fail<T>(Error error)
    {
        return new Result<T?>(default, false, new List<Error> { error });
    }

    public static Result Ok()
    {
        return new Result(true, new List<Error>());
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, new List<Error>());
    }

    public static Result Combine(params Result[] results)
    {
        foreach (var result in results)
        {
            if (result.IsFailure)
                return result;
        }

        return Ok();
    }
}

public class Result<T> : Result
{
    private readonly T _value;

    public T Value
    {
        get
        {
            if (!IsSuccess)
                throw new InvalidOperationException();

            return _value;
        }
    }

    protected internal Result(T value, bool isSuccess, IList<Error> errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }
}