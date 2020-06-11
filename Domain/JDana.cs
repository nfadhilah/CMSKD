using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("JDANA")]
  public class JDana
  {
    [Key, Identity]
    public long IdJDana { get; set; }
    [Key]
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public string Ket { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
