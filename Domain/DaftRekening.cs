using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain
{
  [Table("DAFTREKENING")]
  public class DaftRekening
  {
    [Key, Identity]
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    [Key]
    public int MtgLevel { get; set; }
    [Key]
    public int KdKhusus { get; set; }
    [Key]
    public long IdJRek { get; set; }
    [Key]
    public long IdJnsAkun { get; set; }
    public string Type { get; set; }
    public int StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}