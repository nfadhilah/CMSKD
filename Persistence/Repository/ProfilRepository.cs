using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class ProfilRepository : DapperRepository<Profil>
  {
    public ProfilRepository(IDbConnection connection) : base(connection)
    {
    }

    public ProfilRepository(
      IDbConnection connection, ISqlGenerator<Profil> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}