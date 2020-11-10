using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.WebOtorCQ
{
  public class WebOtorDTO
  {
    public string GroupId { get; set; }
    public string NmGroup { get; set; }
    public string KetGroup { get; set; }
    public string RoleId { get; set; }
    public string NmRole { get; set; }
    public string Type { get; set; }
    public string MenuId { get; set; }
    public string RouterLink { get; set; }
    public string Icon { get; set; }
    public int KdLevel { get; set; }
    public string Bantuan { get; set; }
  }
}