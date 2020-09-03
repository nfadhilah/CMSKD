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
  [Table("METODEPENGADAAN")]
  public class MetodePengadaan
  {
    [Key, Identity]
    public long IdMetodePengadaan { get; set; }

    public string Kode { get; set; }
    public string Uraian { get; set; }
  }
}