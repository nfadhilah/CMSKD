using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.BUD
{
  public class SP2DDetRP
  {
    [Key, Identity]
    public long IdSP2DDetRP { get; set; }
    public long IdSP2DDetR { get; set; }
    [InnerJoin("SP2DDETR", "IDSP2DDETR", "IDSP2DDETR")]
    public SP2DDetR SP2DDetR { get; set; }
    public long IdPajak { get; set; }
    [InnerJoin("PAJAK", "IDPAJAK", "IDPAJAK")]
    public Pajak Pajak { get; set; }
    public decimal? Nilai { get; set; }
    public string Keterangan { get; set; }
    public string IdBilling { get; set; }
    public DateTime? TglBilling { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
