using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TU
{
  [Table("SP2DPJK")]
  public class SP2DPjk
  {
    [Key]
    public string UnitKey { get; set; }
    [Key]
    public string NoSP2D { get; set; }
    [Key]
    public string PjkKey { get; set; }
    [InnerJoin("JPAJAK", "PJKKEY", "PJKKEY")]
    public JPajak JPajak { get; set; }
    public decimal? Nilai { get; set; }
    public string Keterangan { get; set; }
  }
}
