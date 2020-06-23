using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BUD
{
  [Table("SP2DDETRDANA")]
  public class SP2DDetRDana
  {
    [Key, Identity]
    public long IdSP2DDetRDana { get; set; }
    public long IdSP2DDetR { get; set; }
    [InnerJoin("SP2DDETR", "IDSP2DDETR", "IDSP2DDETR")]
    public SP2DDetR SP2DDetR { get; set; }
    public long KdDana { get; set; }
    [InnerJoin("JDANA", "KDDANA", "KDDANA")]
    public JDana JDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
