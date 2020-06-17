using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JDANA")]
  public class JDana
  {
    [Key]
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public string Ket { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
  }
}
