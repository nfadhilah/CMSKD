using System.ComponentModel.DataAnnotations;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.DM
{
  public class Kecamatan
  {
    [Key]
    public string IdKec { get; set; }

    [InnerJoin("KABKOTA", "IDKABKOTA", "IDKABKOTA")]
    public string IdKabKota { get; set; }

    public string Nama { get; set; }
  }
}