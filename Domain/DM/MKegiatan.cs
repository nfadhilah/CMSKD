using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DM
{
  public class MKegiatan
  {
    [Key, Identity]
    public long IdKeg { get; set; }
    public long IdPrgrm { get; set; }
    [InnerJoin("MPGRM", "IDPRGRM", "IDPRGRM")]
    public MPgrm Program { get; set; }
    public string KdPerspektif { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public int LevelKeg { get; set; }
    public string Type { get; set; }
    public long? IdKegInduk { get; set; }
    public bool? StAktif { get; set; }
    public bool? StValid { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
