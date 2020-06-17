namespace Application.Interfaces
{
  public interface IUserAccessor
  {
    string GetCurrentUsername();
    string GetCurrentUserRole();
    long GetCurrentRoleId();
    long GetCurrentAppId();
  }
}
