using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using BCrypt;
using System.Text.RegularExpressions;

// hashing accomplished using Bcrypt for dotnet core
// https://github.com/neoKushan/BCrypt.Net-Core
namespace Fitness_Tracker.Infrastructure.PasswordSecurity
{
    public static class PasswordSecurity
    {
        // private const int SaltByteSize = 128 /;
        public static string HashPassword(string plaintextPassword) =>
            BCrypt.Net.BCrypt.HashPassword(plaintextPassword);

        public static bool CompareHashedPasswords(string suppliedPlaintextPassword, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(suppliedPlaintextPassword, hashedPassword);

        public static bool CheckPasswordPolicies(string plaintextPassword) =>
            ConfirmLengthRequirements(plaintextPassword) && 
            ConfirmSpecialCharacterRequirements(plaintextPassword) &&
            ConfirmNumericRequirements(plaintextPassword) &&
            ConfirmWhitespaceRequirements(plaintextPassword) &&
            ConfirmLowerCaseCharacterRequirements(plaintextPassword) &&
            ConfirmUpperCaseCharacterRequirements(plaintextPassword);

        public static bool ConfirmLengthRequirements(string plaintextPassword) =>
            plaintextPassword.Length >= 8 && plaintextPassword.Length <= 64;

        public static bool ConfirmSpecialCharacterRequirements(string plaintextPassword)
        {
            Regex rx = new Regex("[!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]");
            return rx.IsMatch(plaintextPassword);
        }

        public static bool ConfirmNumericRequirements(string plaintextPassword)
        {
            Regex rx = new Regex("[0-9]");
            return rx.IsMatch(plaintextPassword);
        }

        public static bool ConfirmWhitespaceRequirements(string plaintextPassword)
        {
            Regex rx = new Regex("\\s+");
            return !rx.IsMatch(plaintextPassword);
        }

        public static bool ConfirmLowerCaseCharacterRequirements(string plaintextPassword)
        {
            Regex rx = new Regex("[a-z]");
            return rx.IsMatch(plaintextPassword);
        }

        public static bool ConfirmUpperCaseCharacterRequirements(string plaintextPassword)
        {
            Regex rx = new Regex("[A-Z]");
            return rx.IsMatch(plaintextPassword);
        }
    }
}