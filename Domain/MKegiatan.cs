using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
  public class MKegiatan
  {
    [Key, Identity]
    public long IdKeg { get; set; }
    [Key]
    public long IdPgrm { get; set; }
    public string KdPerspektif { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public int LevelKeg { get; set; }
    public string Type { get; set; }
    public string KdKeg_Induk { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
  }
}
