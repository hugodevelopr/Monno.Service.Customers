using System.Security.Cryptography;
using System.Text;

namespace Monno.SharedKernel.Common;

public static class HashHelper
{
    public static string Create(string input)
    {
        using var sha1 = SHA1.Create();
        var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }
}