using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("PgrmUnit")]
  public class PgrmUnit
  {
    [Key, Identity]
    public long IdPgrmUnit { get; set; }
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit DaftUnit { get; set; }
    public string KdTahap { get; set; }
    public long IdPrgrm { get; set; }
    [LeftJoin("MPGRM", "IDPRGRM", "IDPRGRM")]
    public MPgrm MPgrm { get; set; }
    public string Target { get; set; }
    public string Sasaran { get; set; }
    public int? NoPrio { get; set; }
    public string Indikator { get; set; }
    public string Ket { get; set; }
    public string IdSas { get; set; }
    public DateTime? TglValid { get; set; }
    public int? IdXKode { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}