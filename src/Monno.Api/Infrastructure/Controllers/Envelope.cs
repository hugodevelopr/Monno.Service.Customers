using Monno.SharedKernel;

namespace Monno.Api.Infrastructure.Controllers;

public class Envelope<T>
{
    public string Code { get; }
    public T Payload { get; }
    public bool IsSuccessful { get; }
    public IList<Error> Errors { get; }
    public DateTime GeneratedAt { get; }

    protected internal Envelope(string code, T payload, IList<Error> errors)
    {
        Code = code;
        Payload = payload;
        Errors = errors;
        IsSuccessful = errors == null;
        GeneratedAt = DateTime.Now;
    }

}

public class Envelope : Envelope<string>
{
    protected internal Envelope(string code, IList<Error> errors)
        : base(code, null!, errors)
    {
    }

    public static Envelope<T> Ok<T>(string code, T result)
    {
        return new Envelope<T>(code, result, null!);
    }

    public static Envelope Ok(string code)
    {
        return new Envelope(code, null!);
    }

    public static Envelope Error(string code, IList<Error> errors)
    {
        return new Envelope(code, errors);
    }

    public static Envelope Error(string code, Error error)
    {
        return new Envelope(code, new List<Error>() { error });
    }
}