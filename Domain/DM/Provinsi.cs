using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.DM
{
  [Table("PROVINSI")]
  public class Provinsi
  {
    [Key]
    public string IdProv { get; set; }

    public string Nama { get; set; }
  }

  [Table("KABKOTA")]
  public class KabKota
  {
    [Key]
    public string IdKabKota { get; set; }

    [InnerJoin("PROVINSI", "IDPROV", "IDPROV")]
    public string IdProv { get; set; }

    public string Nama { get; set; }
    public int IdJenis { get; set; }
  }
}