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
    DaftDokRepository DaftDok { get; }
    DocMetaRepository DocMeta { get; }
    SP2DRepository SP2D { get; }
    SP2DDetRRepository Sp2DDetR { get; }
    SP2DDetRTLRepository SP2DDetRTL { get; }
    SP2DPjkRepository SP2DPjk { get; }
    SP2DDetBRepository SP2DDetB { get; }
    SP2DDetDRepository SP2DDetD { get; }
  }
}