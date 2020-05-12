namespace Application.Interfaces
{
  public interface IPasswordHasher
  {
    string Create(string input);
    bool Validate(string hashedInput, string input);
  }
}
