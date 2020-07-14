using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("SPPBPK")]
  public class SPPBPK
  {
    [Key, Identity]
    public long IdSPPBPK { get; set; }
    public long IdSPP { get; set; }
    [InnerJoin("SPP", "IDSPP", "IDSPP")]
    public SPP SPP { get; set; }
    public long IdBPK { get; set; }
    [InnerJoin("BPK", "IDBPK", "IDBPK")]
    public BPK BPK { get; set; }
  }
}
