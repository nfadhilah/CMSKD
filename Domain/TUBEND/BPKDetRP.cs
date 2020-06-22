using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BPKDETRP")]
  public class BPKDetRP
  {
    [Key, Identity]
    public long IdBPKDetRP { get; set; }
    public long IdBPKDetR { get; set; }
    [InnerJoin("BPKDETR", "IDBPKDETR", "IDBPKDETR")]
    public BPKDetR BPKDetR { get; set; }
    public long IdPajak { get; set; }
    [InnerJoin("PAJAK", "IDPAJAK", "IDPAJAK")]
    public Pajak Pajak { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}