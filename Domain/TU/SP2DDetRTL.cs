using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.TU
{
  [Table("SP2DDETRTL")]
  public class SP2DDetRTL
  {
    [Key]
    public string MtgKey { get; set; }
    [InnerJoin("MATANGR", "MTGKEY", "MTGKEY")]
    public MatangR MatangR { get; set; }
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