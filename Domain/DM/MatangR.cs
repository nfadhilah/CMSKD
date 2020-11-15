using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DM
{
  [Table("MATANGR")]
  public class MatangR
  {
    [Key] public string MtgKey { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public string MtgLevel { get; set; }
    public string KdKhusus { get; set; }
    public string Type { get; set; }
  }
}