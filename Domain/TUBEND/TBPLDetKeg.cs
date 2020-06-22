using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("TBPLDETKEG")]
  public class TBPLDetKeg
  {
    [Key, Identity]
    public long IdTBPLDetKeg { get; set; }
    public long IdTBPLDet { get; set; }
    [InnerJoin("TBPLDET", "IDTBLDET", "IDTBLDET")]
    public TBPLDet TBPLDet { get; set; }
    public int IdNoJeTra { get; set; }
    [InnerJoin("JTRNLKAS", "IDNOJETRA", "IDNOJETRA")]
    public JTrnlKas JTrnlKas { get; set; }
    public long IdKeg { get; set; }
    [InnerJoin("MKEGIATAN", "IDKEG", "IDKEG")]
    public MKegiatan Kegiatan { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
