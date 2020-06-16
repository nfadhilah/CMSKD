using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.DM
{
  [Table("BKBKAS")]
  public class BkBKas
  {
    [Key, Identity]
    public long IdKas { get; set; }
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit DaftUnit { get; set; }
    public long IdRek { get; set; }
    [LeftJoin("DAFTREKENING", "IDREK", "IDREK")]
    public DaftRekening DaftRekening { get; set; }
    public long IdBank { get; set; }
    public string NmBKas { get; set; }
    public string NoRek { get; set; }
    public Decimal? Saldo { get; set; }
  }
}