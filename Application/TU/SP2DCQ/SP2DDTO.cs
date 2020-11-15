using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TU.SP2DCQ
{
  public class SP2DDTO 
  {
    public string UnitKey { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string NoSP2D { get; set; }
    public string KdStatus { get; set; }
    public string LblStatus { get; set; }
    public string NoSPM { get; set; }
    public string KeyBend { get; set; }
    public string IdxSKO { get; set; }
    public string IdxTTD { get; set; }
    public string KdP3 { get; set; }
    public string IdxKode { get; set; }
    public string NoReg { get; set; }
    public string KetOtor { get; set; }
    public string NoKontrak { get; set; }
    public string Keperluan { get; set; }
    public string Penolakan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? TglSP2D { get; set; }
    public DateTime? TglSPM { get; set; }
    public string NobBantu { get; set; }
  }
}