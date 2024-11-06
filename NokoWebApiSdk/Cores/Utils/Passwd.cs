using System;
using System.Security.Cryptography;

namespace NokoWebApiSdk.Cores.Utils;

public static class Passwd
{
    // Constants for PBKDF2 parameters
    private const int N = 10000;           // Number of iterations for the key derivation
    private const int SaltSize = 16;       // Size of the salt in bytes
    private const int KeyLength = 32;      // Length of the derived key in bytes
    private const string SeparatorChar = "$";  // Separator character for encoding
    private const string PrefixWord = "PDKF2_"; // Prefix for the hashed password

    /// <summary>
    /// Hashes a password using PBKDF2 with SHA256.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>A URL-safe, base64-encoded hash string with a salt and prefix.</returns>
    public static string HashPassword(string password)
    {
        var salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt); // Generate a cryptographic random salt

        var key = new Rfc2898DeriveBytes(password, salt, N, HashAlgorithmName.SHA256).GetBytes(KeyLength); // Derive the key
        var temp = PrefixWord + NokoCommonMod.EncodeBase64UrlSafe(salt) + SeparatorChar + NokoCommonMod.EncodeBase64UrlSafe(key); // Combine and encode
        return temp;
    }

    /// <summary>
    /// Compares a hashed password with a plain text password.
    /// </summary>
    /// <param name="hash">The hashed password (including salt and prefix).</param>
    /// <param name="password">The plain text password to compare.</param>
    /// <returns>True if the passwords match, false otherwise.</returns>
    public static bool CompareHashPassword(string hash, string password)
    {
        // Check if the hash starts with the expected prefix
        if (hash.StartsWith(PrefixWord))
        {
            hash = hash[PrefixWord.Length..]; // Remove the prefix from the hash
        }
        else
        {
            return false; // Invalid prefix
        }

        // Check if the hash contains the separator character
        if (!hash.Contains(SeparatorChar))
        {
            return false; // Separator not found
        }

        // Split the hash into salt and key components
        var tokens = hash.Split(new[] { SeparatorChar }, StringSplitOptions.None);
        if (tokens.Length != 2)
        {
            return false; // Incorrect format
        }

        // Decode the salt and stored key from Base64 URL-safe encoding
        var salt = NokoCommonMod.DecodeBase64UrlSafe(tokens[0]);
        var storedKey = NokoCommonMod.DecodeBase64UrlSafe(tokens[1]);

        // Derive the key from the provided password and decoded salt
        var derivedKey = new Rfc2898DeriveBytes(password, salt, N, HashAlgorithmName.SHA256).GetBytes(KeyLength);
        
        // Compare the stored key with the derived key
        return storedKey.SequenceEqual(derivedKey);
    }
}
