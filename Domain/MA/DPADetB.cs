using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("DPADetB")]
  public class DPADetB
  {
    [Key, Identity]
    public long IdDPADetB { get; set; }
    public long IdDPAB { get; set; }
    [LeftJoin("DPAB", "IDDPAB", "IDDPAB")]
    public DPAB DPAB { get; set; }
    public string KdNilai { get; set; }
    public string KdJabar { get; set; }
    public string Uraian { get; set; }
    public decimal? JumBYek { get; set; }
    public string Satuan { get; set; }
    public decimal? Tarif { get; set; }
    public decimal? SubTotal { get; set; }
    public string Ekspresi { get; set; }
    public byte InclSubtotal { get; set; }
    public string Type { get; set; }
    public string IdStdHarga { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}