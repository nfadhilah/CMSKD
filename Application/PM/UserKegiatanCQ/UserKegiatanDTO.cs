using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PM.UserKegiatanCQ
{
  public class UserKegiatanDTO
  {
    public string UserId { get; set; }
    public string Nama { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public string AssignBy { get; set; }
    public DateTime? AssignDate { get; set; }
  }
}