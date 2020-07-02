using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("PROFIL")]
  public class Profil
  {
    [Key, Identity]
    public long IdProfil { get; set; }
    public string KdProfil { get; set; }
    public string NmProfil { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
