using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("TBPLDET")]
  public class TBPLDet
  {
    [Key, Identity]
    public long IdTBPLDet { get; set; }
    public long IdTBPL { get; set; }
    [InnerJoin("TBPL", "IdTBPL", "IdTBPL")]
    public TBPL TBPL { get; set; }
    public long IdBend { get; set; }
    [InnerJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bend { get; set; }
    public int IdNoJeTra { get; set; }
    [InnerJoin("JTRNLKAS", "IDNOJETRA", "IDNOJETRA")]
    public JTrnlKas JTrnlKas { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}