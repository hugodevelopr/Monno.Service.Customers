namespace Monno.SharedKernel;

[Serializable]
public class Error
{
    public Error()
        : this(string.Empty, string.Empty)
    {
    }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }
}