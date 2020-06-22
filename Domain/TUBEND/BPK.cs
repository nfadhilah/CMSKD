using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BPK")]
  public class BPK
  {
    [Key, Identity]
    public long IdBPK { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public long IdPhk3 { get; set; }
    [LeftJoin("DAFTPHK3", "IDPHK3", "IDPHK3")]
    public DaftPhk3 Phk3 { get; set; }
    public string NoBPK { get; set; }
    public string KdStatus { get; set; }
    public string JBayar { get; set; }
    public int IdxKode { get; set; }
    public long IdBend { get; set; }
    [LeftJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bend { get; set; }
    public DateTime? TglBPK { get; set; }
    public string UraiBPK { get; set; }
    public DateTime? TglValid { get; set; }
    public long? IdBerita { get; set; }
    [LeftJoin("BERITA", "IDBERITA", "IDBERITA")]
    public Berita Berita { get; set; }
    public long? KdRilis { get; set; }
    public int? StKirim { get; set; }
    public int? StCair { get; set; }
    public string NoRef { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
