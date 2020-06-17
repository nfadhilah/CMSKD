using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("PPK")]
  public class PPK
  {
    [Key, Identity]
    public long IdPPK { get; set; }
    public long IdPeg { get; set; }
    [LeftJoin("PEGAWAI", "IDPEG", "IDPEG")]
    public Pegawai Pegawai { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}