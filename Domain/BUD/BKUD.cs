using Domain.DM;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BUD
{
  [Table("BKUD")]
  public class BKUD
  {
    [Key, Identity]
    public long IdBKUD { get; set; }
    public long? IdKas { get; set; }
    public long IdSTS { get; set; }
    [LeftJoin("STS", "IDSTS", "IDSTS")]
    public STS STS { get; set; }
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
