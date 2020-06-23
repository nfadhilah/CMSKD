using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BUD
{
  [Table("BKUK")]
  public class BKUK
  {
    [Key, Identity]
    public long IdBKUK { get; set; }
    public long? IdKas { get; set; }
    public long IdSP2D { get; set; }
    [InnerJoin("SP2D", "IDSP2D", "IDSP2D")]
    public SP2D SP2D { get; set; }
    public long? IdBKas { get; set; }
    public long? IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public DateTime? TglKas { get; set; }
    public DateTime? TglValid { get; set; }
    public string Uraian { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}