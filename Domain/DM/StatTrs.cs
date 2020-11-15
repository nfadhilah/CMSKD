using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DM
{
  [Table("STATTRS")]
  public class StatTrs
  {
    [Key]
    public string KdStatus { get; set; }
    public string LblStatus { get; set; }
    public string Uraian { get; set; }
  }
}