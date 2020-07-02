using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("DAFTUNIT")]
  public class DaftUnit
  {
    [Key, Identity]
    public long IdUnit { get; set; }
    public long IdUrus { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public int KdLevel { get; set; }
    public string Type { get; set; }
    public string AkroUnit { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public int StAktif { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
