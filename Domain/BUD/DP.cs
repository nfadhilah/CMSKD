using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BUD
{
  [Table("DP")]
  public class DP
  {
    [Key, Identity]
    public long IdDP { get; set; }
    public string NoDP { get; set; }
    public int? IdxKode { get; set; }
    public long? IdTtd { get; set; }
    public DateTime? TglDP { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
