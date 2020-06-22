using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("SPPDETBDANA")]
  public class SPPDetBDana
  {
    [Key, Identity]
    public long IdSPPDetBDana { get; set; }
    public long IdSPPDetB { get; set; }
    [InnerJoin("SPPDETB", "IDSPPDETB", "IDSPPDETB")]
    public SPPDetB SPPDetB { get; set; }
    public string KdDana { get; set; }
    [LeftJoin("JDANA", "KDDANA", "KDDANA")]
    public JDana JDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
