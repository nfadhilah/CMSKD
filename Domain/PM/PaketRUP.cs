using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.PM
{
  [Table("PAKETRUP")]
  public class PaketRUP
  {
    [Key, Identity]
    public long IdRUP { get; set; }

    public long IdUnit { get; set; }

    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }

    public long IdKeg { get; set; }

    [InnerJoin("MKEGIATAN", "IDKEG", "IDKEG")]
    public MKegiatan Keg { get; set; }

    public decimal? NilaiPagu { get; set; }
    public DateTime? TglValid { get; set; }

    [UpdatedAt]
    public DateTime? DateCreate { get; set; }

    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}