using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.PM
{
  [Table("DISKUSIPAKET")]
  public class DiskusiPaket
  {
    [Key, Identity]
    public int IdDiskusiPaket { get; set; }

    public string Komentar { get; set; }
    public string Sender { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    public long IdRUP { get; set; }
  }
}