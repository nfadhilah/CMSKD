using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DM
{
  [Table("BEND")]
  public class Bend
  {
    [Key] public string KeyBend { get; set; }
    public string Jns_Bend { get; set; }
    public string NIP { get; set; }
    public string KdBank { get; set; }
    public string UnitKey { get; set; }
    public string Jab_Bend { get; set; }
    public string RekBend { get; set; }
    public string SaldoBend { get; set; }
    public string NPWPBend { get; set; }
    public string TglStopBend { get; set; }
    public decimal? SaldoBendT { get; set; }
  }
}