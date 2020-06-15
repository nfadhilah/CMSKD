using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("KPA")]
  public class KPA
  {
    [Key, Identity]
    public long IdKPA { get; set; }
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT", TableAlias = "Unit")]
    public DaftUnit DaftUnit { get; set; }
    public long IdPeg { get; set; }
    [LeftJoin("PEGAWAI", "IDPEG", "IDPEG", TableAlias = "Pegawai")]
    public Pegawai Pegawai { get; set; }
    public string Jabatan { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
  }
}
