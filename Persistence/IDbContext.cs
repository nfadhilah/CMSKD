using System;
using System.Data;
using Persistence.Repository.Auth;
using Persistence.Repository.DM;

namespace Persistence
{
  public interface IDbContext : IDisposable
  {
    IDbConnection Connection { get; }
    WebUserRepository WebUser { get; }
    WebGroupRepository WebGroup { get; }
    WebRoleRepository WebRole { get; }
    WebOtorRepository WebOtor { get; }
    TahunRepository Tahun { get; }
    PegawaiRepository Pegawai { get; }
  }
}