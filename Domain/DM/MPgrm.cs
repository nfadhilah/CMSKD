using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("MPGRM")]
  public class MPgrm
  {
    [Key, Identity]
    public long IdPrgrm { get; set; }
    public long IdUrus { get; set; }
    public string NmPrgrm { get; set; }
    public string NuPrgrm { get; set; }
    public string IdPrioda { get; set; }
    public string IdPrioProv { get; set; }
    public string IdPrioNas { get; set; }
    public int? IdxKode { get; set; }
    public bool? StAktif { get; set; }
    public bool? StValid { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
  }
}