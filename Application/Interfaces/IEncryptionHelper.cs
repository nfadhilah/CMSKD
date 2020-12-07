namespace Application.Interfaces
{
  public interface IEncryptionHelper
  {
    string Encrypt(string clearText);
    string Decrypt(string cipherText);
  }
}
