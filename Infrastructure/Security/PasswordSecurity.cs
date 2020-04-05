using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using BCrypt;

// hashing accomplished using Bcrypt for dotnet core
// https://github.com/neoKushan/BCrypt.Net-Core
namespace Fitness_Tracker.Infrastructure.PasswordSecurity
{
    public static class PasswordSecurity
    {
        // private const int SaltByteSize = 128 /;
        public static string HashPassword(string plaintextPassword) =>
            BCrypt.Net.BCrypt.HashPassword(plaintextPassword);
        public static string HashEmail(string plaintextEmail) =>
            BCrypt.Net.BCrypt.HashString(plaintextEmail);

        public static bool CompareHashedPasswords(string suppliedPlaintextPassword, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(suppliedPlaintextPassword, hashedPassword);

        // TODO when the password policy has been decided, a regex should be used to
        // test the validity of the password according to the policy
        public static bool CheckPasswordPolicy(string plaintextPassword)
        {
            return true;
        }

    }
}