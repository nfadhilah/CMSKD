using System.ComponentModel.DataAnnotations;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.DM
{
  public class Kelurahan
  {
    [Key]
    public string IdKel { get; set; }

    [InnerJoin("KECAMATAN", "IDKEC", "IDKEC")]
    public string IdKec { get; set; }

    public string Nama { get; set; }
    public int IdJenis { get; set; }
  }
}