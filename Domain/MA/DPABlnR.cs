using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("DPABlnR")]
  public class DPABlnR
  {
    [Key, Identity]
    public long IdDPABlnR { get; set; }
    public long IdDPAR { get; set; }
    [LeftJoin("DPAR", "IDDPAR", "IDDPAR")]
    public DPAR DPAR { get; set; }
    public long IdBulan { get; set; }
    public decimal? Nilai { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}