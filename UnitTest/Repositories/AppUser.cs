using Domain;
using Persistence;
using System.Collections.Generic;
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
<<<<<<< HEAD
        "Server=.;User Id=usadi;Password=valid49;Database=V@LID49V7_2020;Trusted_Connection=False;");
=======
        "Server=.;User Id=sa;Password=1234;Database=V@LID49V6_2020_CASHLESS;Trusted_Connection=False;");
>>>>>>> dev
    }

    [Fact]
    public void FindAllUsers()
    {
      var user = _db.AppUser.FindAll().ToList();
      Assert.NotEmpty(user);
      Assert.Equal(2, user.Count);
    }

    [Fact]
    public void FindAllUsersJoinWithUserRoles()
    {
      var user = _db.AppUser
        .FindAll<UserRoles>(x => x.UserName == "admin", q => q.UserRoles)
        .First();
      Assert.Equal(2, user.UserRoles.Count);
    }

    [Fact]
    public void FindAllByListOfName()
    {
      var names = new List<string> { "admin", "op" };
      var user = _db.AppUser.FindAll(u => names.Contains(u.UserName)).ToArray();
      Assert.Equal("admin", user[0].UserName);
      Assert.Equal("op", user[1].UserName);
    }
  }
}