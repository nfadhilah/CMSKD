using Persistence;
using System.Linq;
using Xunit;

namespace UnitTest.Repositories
{
  public class AppUser
  {
    private readonly IDbContext _db;

    public AppUser()
    {
      _db = new DbContext(
        "Server=.;User Id=usadi;Password=valid49;Database=V@LID49V7_2020;Trusted_Connection=False;");
    }

    [Fact]
    public void FindAllUsers()
    {
      var user = _db.WebUser.FindAll().ToList();
      Assert.NotEmpty(user);
      Assert.Equal(2, user.Count);
    }
  }
}