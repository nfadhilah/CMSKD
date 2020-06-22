using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("DPABlnB")]
  public class DPABlnB
  {
    [Key, Identity]
    public long IdDPABlnB { get; set; }
    public long IdDPAB { get; set; }
    [LeftJoin("DPAB", "IDDPAB", "IDDPAB")]
    public DPAB DPAB { get; set; }
    public long IdBulan { get; set; }
    public Decimal? Nilai { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}