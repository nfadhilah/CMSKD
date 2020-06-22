using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BKPAJAKDETSTR")]
  public class BkPajakDetStr
  {
    [Key, Identity]
    public long IdBkPajakDetStr { get; set; }
    public long IdBkPajakStr { get; set; }
    [LeftJoin("BPKPAJAKSTR", "IDBKPAJAKSTR", "IDBKPAJAKSTR")]
    public BPKPajakStr BPKPajakStr { get; set; }
    public long? IdPajak { get; set; }
    [InnerJoin("PAJAK", "IDPAJAK", "IDPAJAK")]
    public Pajak Pajak { get; set; }
    public long IdBkPajak { get; set; }
    [InnerJoin("BKPAJAK", "IDBKPAJAK", "IDBKPAJAK")]
    public BkPajak BkPajak { get; set; }
    public string IdBilling { get; set; }
    public DateTime? TglIdBilling { get; set; }
    public DateTime? TglExpire { get; set; }
    public string NTPN { get; set; }
    public string NTB { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}