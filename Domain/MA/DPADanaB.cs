using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("DPADanaB")]
  public class DPADanaB
  {
    [Key, Identity]
    public long IdDPADanaB { get; set; }
    public long IdDPAB { get; set; }
    [LeftJoin("DPAB", "IDDPAB", "IDDPAB")]
    public DPAB DPAB { get; set; }
    public string KdDana { get; set; }
    public Decimal? Nilai { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}