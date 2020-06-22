using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BKBANKDET")]
  public class BkBankDet
  {
    [Key, Identity]
    public long IdBankDet { get; set; }
    public long IdBkBank { get; set; }
    [InnerJoin("BKBANK", "IDBKBANK", "IDBKBANK")]
    public BkBank BkBank { get; set; }
    public int NoJeTra { get; set; }
    [InnerJoin("JTRNLKAS", "IDNOJETRA", "IDNOJETRA")]
    public JTrnlKas JTrnlKas { get; set; }
    public decimal? Nilai { get; set; }
  }
}