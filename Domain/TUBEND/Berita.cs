using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BERITA")]
  public class Berita
  {
    [Key, Identity]
    public long IdBerita { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public long IdKeg { get; set; }
    [InnerJoin("MKEGIATAN", "IDKEG", "IDKEG")]
    public MKegiatan Kegiatan { get; set; }
    public string NoBerita { get; set; }
    public DateTime TglBA { get; set; }
    public long IdKontrak { get; set; }
    [LeftJoin("KONTRAK", "IDKONTRAK", "IDKONTRAK")]
    public Kontrak Kontrak { get; set; }
    [NotMapped]
    public DaftPhk3 Phk3 { get; set; }
    public string Urai_Berita { get; set; }
    public DateTime? TglValid { get; set; }
    public string KdStatus { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
