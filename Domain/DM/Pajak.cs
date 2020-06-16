using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
{
  [Table("PAJAK")]
  public class Pajak
  {
    [Key, Identity]
    public long IdPjk { get; set; }
    public string KdPajak { get; set; }
    public string NmPajak { get; set; }
    public string Uraian { get; set; }
    public string Keterangan { get; set; }
    public string RumusPajak { get; set; }
    public int StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
