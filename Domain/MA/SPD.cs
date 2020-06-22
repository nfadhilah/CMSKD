using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.MA
{
  public class SPD
  {
    [Key, Identity]
    public long IdSPD { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public string NoSPD { get; set; }
    public DateTime? TglSPD { get; set; }
    public int KdBulan1 { get; set; }
    public int KdBulan2 { get; set; }
    public int IdxKode { get; set; }
    public string Keterangan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
