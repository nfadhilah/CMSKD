using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BUD
{
  [Table("SP2DDETBDANA")]
  public class SP2DDetBDana
  {
    [Key, Identity]
    public long IdSP2DDetBDana { get; set; }
    public long IdSP2DDetB { get; set; }
    [InnerJoin("SP2DDETB", "IDSP2DDETB", "IDSP2DDETB")]
    public SP2DDetB SP2DDetB { get; set; }
    public long KdDana { get; set; }
    [InnerJoin("JDANA", "KDDANA", "KDDANA")]
    public JDana JDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
