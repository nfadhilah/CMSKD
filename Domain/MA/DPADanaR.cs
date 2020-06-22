using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("DPADanaR")]
  public class DPADanaR
  {
    [Key, Identity]
    public long IdDPADanaR { get; set; }
    public long IdDPAR { get; set; }
    [LeftJoin("DPAR", "IDDPAR", "IDDPAR")]
    public DPAR DPAR { get; set; }
    public string KdDana { get; set; }
    public Decimal? Nilai { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}