using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("PAJAK")]
  public class Pajak
  {
    [Key, Identity]
    public long IdPajak { get; set; }
    public string KdPajak { get; set; }
    public string NmPajak { get; set; }
    public string Uraian { get; set; }
    public string Keterangan { get; set; }
    public string RumusPajak { get; set; }
    public int? IdJnsPajak { get; set; }
    public int? StAktif { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
