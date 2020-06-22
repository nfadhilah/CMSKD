using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("SPDDETR")]
  public class SPDDetR
  {
    [Key, Identity]
    public long IdSPDDetR { get; set; }
    public long IdSPD { get; set; }
    [InnerJoin("SPD", "IDSPD", "IDSPD")]
    public SPD SPD { get; set; }
    public long IdKeg { get; set; }
    [InnerJoin("MKEGIATAN", "IDKEG", "IDKEG")]
    public MKegiatan Kegiatan { get; set; }
    public long IdRek { get; set; }
    [InnerJoin("DAFTREKENING", "IDREK", "IDREK")]
    public DaftRekening Rekening { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}