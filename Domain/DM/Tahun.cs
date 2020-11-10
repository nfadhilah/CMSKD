using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DM
{
  [Table("TAHUN")]
  public class Tahun
  {
    [Key]
    public string KdTahun { get; set; }
    public string NmTahun { get; set; }
  }
}