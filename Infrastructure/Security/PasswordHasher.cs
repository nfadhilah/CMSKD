using Application.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security
{
  public class PasswordHasher : IPasswordHasher
  {
    public string Create(string input)
    {
      byte[] inputSalt, inputHash;

      using (var hmac = new HMACSHA1())
      {
        inputSalt = hmac.Key;
        inputHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
      }

      return $"{Convert.ToBase64String(inputSalt)}:{Convert.ToBase64String(inputHash)}";
    }

    public bool Validate(string hashedInput, string input)
    {
      var parts = hashedInput.Split(':');

      var inputSalt = Convert.FromBase64String(parts[0]);

      var inputHash = Convert.FromBase64String(parts[1]);

      using var hmac = new HMACSHA1(inputSalt);

      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));

      return !computedHash.Where((t, i) => t != inputHash[i]).Any();
    }
  }
}
