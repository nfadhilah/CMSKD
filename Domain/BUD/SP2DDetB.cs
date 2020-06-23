using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BUD
{
  [Table("SP2DDETB")]
  public class SP2DDetB
  {
    [Key, Identity]
    public long IdSP2DDetB { get; set; }
    public long IdSP2D { get; set; }
    [InnerJoin("SP2D", "IDSP2D", "IDSP2D")]
    public SP2D SP2D { get; set; }
    public long IdRek { get; set; }
    [InnerJoin("DAFTREKENING", "IDREK", "IDREK")]
    public DaftRekening Rekening { get; set; }
    public int IdNoJeTra { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
