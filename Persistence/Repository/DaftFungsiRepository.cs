﻿using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class DaftFungsiRepository : DapperRepository<DaftFungsi>
  {
    public DaftFungsiRepository(IDbConnection connection) : base(connection)
    {
    }

    public DaftFungsiRepository(
      IDbConnection connection, ISqlGenerator<DaftFungsi> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}