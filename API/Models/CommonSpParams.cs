using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
  public class CommonSpParams
  {
    [Required]
    public string SpName { get; set; }

    public Dictionary<string, object> Parameters { get; set; }
  }
}