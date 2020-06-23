using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JABTTD")]
  public class JabTtd
  {
    [Key, Identity]
    public long IdTtd { get; set; }
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit DaftUnit { get; set; }
    public long IdPeg { get; set; }
    [LeftJoin("PEGAWAI", "IDPEG", "IDPEG")]
    public Pegawai Pegawai { get; set; }
    public string KdDok { get; set; }
    public string Jabatan { get; set; }
    public string NoSKPTtd { get; set; }
    public DateTime? TglSKPTtd { get; set; }
    public string NoSKStopTtd { get; set; }
    public DateTime? TglSKStopTtd { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
