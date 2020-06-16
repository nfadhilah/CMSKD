using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
{
  [Table("PROFIL")]
  public class Profil
  {
    [Key, Identity]
    public long IdProfil { get; set; }
    [Key]
    public string KdProfil { get; set; }
    public string NmProfil { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
