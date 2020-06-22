using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("SPPDETB")]
  public class SPPDetB
  {
    [Key, Identity]
    public long IdSPPDetB { get; set; }
    public long IdRek { get; set; }
    [InnerJoin("DAFTREKENING", "IDREK", "IDREK")]
    public DaftRekening Rekening { get; set; }
    public long IdSPP { get; set; }
    [InnerJoin("SPP", "IDSPP", "IDSPP")]
    public SPP SPP { get; set; }
    public int IdNoJeTra { get; set; }
    [InnerJoin("JTRNLKAS", "IDNOJETRA", "IDNOJETRA")]
    public JTrnlKas JTrnlKas { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime DateUpdate { get; set; }
  }
}