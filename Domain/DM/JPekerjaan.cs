using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
{
  [Table("JPEKERJAAN")]
  public class JPekerjaan
  {
    [Key, Identity]
    public long IdJnsPekerjaan { get; set; }

    public string Uraian { get; set; }
  }
}