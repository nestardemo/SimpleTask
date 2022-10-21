using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace SimpleWebApi.Domain
{
    public class User
    {
        public Guid UserId { get; private set; }
        public string Login { get; private set; }
        public Guid ProvinceId { get; private set; }
        public Province Province { get; private set; }
        public string PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public User(string login, Guid provinceId, string passwordHash)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

            Login = login;
            ProvinceId = provinceId;
            PasswordHash = GetHashedPassword(passwordHash, salt);
            PasswordSalt = salt;
        }

        public void SetLogin(string login)
        {
            Login = login;
        }

        public void SetProvince(Province province)
        {
            Province = province;
        }

        private string GetHashedPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                   KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
        }

    }
}