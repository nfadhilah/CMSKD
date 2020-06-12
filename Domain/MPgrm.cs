using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("MPGRM")]
  public class MPgrm
  {
    [Key, Identity]
    public long IdPrgrm { get; set; }
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IdUnit", "IdUnit")]
    public DaftUnit Urusan { get; set; }
    public string NmPrgrm { get; set; }
    public string NuPrgrm { get; set; }
    public string IdPrioda { get; set; }
    public string IdPrioProv { get; set; }
    public string IdPrioNas { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
  }
}