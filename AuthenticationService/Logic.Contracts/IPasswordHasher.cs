using System.Security.Cryptography;

namespace AuthenticationService.Logic.Contracts
{
    public interface IPasswordHasher
    {
        HashAlgorithmName HashAlgorithmName { get; }
        int Pbkdf2IterCount { get; }
        int Pbkdf2SubkeyLength { get; }
        int SaltSize { get; }
        byte Version { get; }

        string HashPassword(string password);
        PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string password);
    }
}