using Persistence.Repository.Auth;
using Persistence.Repository.DM;
using Persistence.Repository.TU;
using System;
using System.Data;

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
    STSRepository STS { get; }
    RkmDetDRepository RkmDetD { get; }
    BKUSTSRepository BKUSTS { get; }
    BKUDRepository BKUD { get; }
  }
}