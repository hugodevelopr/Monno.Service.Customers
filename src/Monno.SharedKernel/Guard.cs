namespace Monno.SharedKernel;

public static class Guard
{
    public static void NotNull(this object obj, string paramName)
    {
        if (obj == null)
            throw new ArgumentNullException(paramName, "The object can't be null");
    }

    public static void NotNullOrEmpty(this object obj, string paramName)
    {
        NotNull(obj, paramName);

        if (obj is string str && string.IsNullOrEmpty(str))
            throw new ArgumentException("The object can't be empty");
    }
}