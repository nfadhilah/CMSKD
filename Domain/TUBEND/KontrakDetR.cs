using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("KONTRAKDETR")]
  public class KontrakDetR
  {
    [Key, Identity]
    public long IdDetKontrak { get; set; }
    public long IdKontrak { get; set; }
    public long IdRek { get; set; }
    public int IdBulan { get; set; }
    public long IdJTermOrLun { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
