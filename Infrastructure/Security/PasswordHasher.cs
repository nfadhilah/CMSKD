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
      var md5 = new MD5CryptoServiceProvider();
      var encoding = Encoding.GetEncoding("Windows-1252");
      var result = md5.ComputeHash(encoding.GetBytes(input));
      return encoding.GetString(result);
    }

    public bool Validate(string hashedInput, string input)
    {
      return Create(input) == hashedInput;
    }
  }
}
