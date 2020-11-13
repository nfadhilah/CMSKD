using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.DTOS
{
  public class CommonSpParams
  {
    [Required]
    public string SpName { get; set; }

    public Dictionary<string, object> Parameters { get; set; }
  }
}