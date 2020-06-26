using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("WEBSET")]
  public class WebSet
  {
    public long IdWebSet { get; set; }
    public string KdSet { get; set; }
    public string ValSet { get; set; }
    public string ValDesc { get; set; }
    public int? ModeEntry { get; set; }
    public string ValList { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
