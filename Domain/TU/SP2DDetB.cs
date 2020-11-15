using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TU
{
  [Table("SP2DDETB")]
  public class SP2DDetB
  {
    [Key]
    public string MtgKey { get; set; }
    [InnerJoin("MATANGB", "MTGKEY", "MTGKEY")]
    public MatangB MatangB { get; set; }
    [Key]
    public string UnitKey { get; set; }
    [Key]
    public string NoSP2D { get; set; }
    [Key]
    public string NoJeTra { get; set; }
    public string KdDana { get; set; }
    [LeftJoin("JDANA", "KDDANA", "KDDANA")]
    public JDana JDana { get; set; }
    public decimal? Nilai { get; set; }
  }
}