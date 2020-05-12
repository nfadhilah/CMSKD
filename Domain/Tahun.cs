using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("TAHUN")]
  public class Tahun
  {
    [Key, Identity]
    public int Id { get; set; }

    public string NmTahun { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime DateUpdate { get; set; }
  }
}
