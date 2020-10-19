using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth
{
    public class WebGroup
    {
      [Key]
      public string GroupId { get; set; }
      public string NmGroup { get; set; }
      public string Ket { get; set; }
    }
}
