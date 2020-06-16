using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
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
