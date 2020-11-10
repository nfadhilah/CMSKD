namespace Application.Interfaces
{
  public interface IUserAccessor
  {
    string GetCurrentUsername();
    string GetCurrentUserRole();
    string GetCurrentRoleId();
    int GetCurrentAppId();
  }
}
