using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("KONTRAK")]
  public class Kontrak
  {
    [Key, Identity]
    public long IdKontrak { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public string NoKontrak { get; set; }
    public long IdPhk3 { get; set; }
    [LeftJoin("DAFTPHK3", "IDPHK3", "IDPHK3")]
    public DaftPhk3 Phk3 { get; set; }
    public long IdKeg { get; set; }
    [InnerJoin("MKEGIATAN", "IDKEG", "IDKEG")]
    public MKegiatan Kegiatan { get; set; }
    public DateTime? TglKon { get; set; }
    public DateTime? TglAwalKontrak { get; set; }
    public DateTime? TglAkhirKontrak { get; set; }
    public DateTime? TglSlsKontrak { get; set; }
    public string Uraian { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
