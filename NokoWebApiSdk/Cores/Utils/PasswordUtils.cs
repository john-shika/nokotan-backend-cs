using System;
using System.Security.Cryptography;

namespace NokoWebApiSdk.Cores.Utils;

public static class PasswordUtils
{
    private const int N = 10000;
    private const int SaltSize = 16;
    private const int KeyLength = 32;
    private const string SeparatorChar = "$";
    private const string PrefixWord = "PDKF2_";

    public static string HashPassword(string password)
    {
        var salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        var key = new Rfc2898DeriveBytes(password, salt, N, HashAlgorithmName.SHA256).GetBytes(KeyLength);
        var temp = PrefixWord + Convert.ToBase64String(salt) + SeparatorChar + Convert.ToBase64String(key);
        return temp;
    }

    public static bool CompareHashPassword(string hash, string password)
    {
        if (hash.StartsWith(PrefixWord))
        {
            hash = hash[PrefixWord.Length..];
        }
        else
        {
            return false;
        }

        if (!hash.Contains(SeparatorChar))
        {
            return false;
        }

        var tokens = hash.Split(new[] { SeparatorChar }, StringSplitOptions.None);
        if (tokens.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(tokens[0]);
        var pass = Convert.FromBase64String(tokens[1]);

        var key = new Rfc2898DeriveBytes(password, salt, N, HashAlgorithmName.SHA256).GetBytes(KeyLength);
        return pass.Equals(key);
    }
}

