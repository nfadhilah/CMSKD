using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("SPPDETRP")]
  public class SPPDetRP
  {
    [Key, Identity]
    public long IdSPPDetRP { get; set; }
    public long IdSPPDetR { get; set; }
    [InnerJoin("SPPDETR", "IDSPPDETR", "IDSPPDETR")]
    public SPPDetR SPPDetR { get; set; }
    public long IdPajak { get; set; }
    [LeftJoin("PAJAK", "IDPAJAK", "IDPAJAK")]
    public Pajak Pajak { get; set; }
    public decimal? Nilai { get; set; }
    public string Keterangan { get; set; }
    public string IdBilling { get; set; }
    public DateTime? TglBilling { get; set; }
    public string NTPN { get; set; }
    public string NTB { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}