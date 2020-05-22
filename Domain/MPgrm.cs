using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("MPGRM")]
  public class MPgrm
  {
    [Key]
    public string IdPrgrm { get; set; }

    public string UnitKey { get; set; }

    [LeftJoin("DAFTUNIT", "UnitKey", "UnitKey")]
    public DaftUnit Urusan { get; set; }

    public string NmPrgrm { get; set; }
    public string NuPrgrm { get; set; }
  }
}