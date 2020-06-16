using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain
{
  [Table("PPK")]
  public class PPK
  {
    [Key, Identity]
    public long IdPPK { get; set; }
    [Key]
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit DaftUnit { get; set; }
    public long IdPeg { get; set; }
    [LeftJoin("PEGAWAI", "IDPEG", "IDPEG")]
    public Pegawai Pegawai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}