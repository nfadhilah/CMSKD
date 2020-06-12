using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
  public class DaftRekening
  {
    [Key, Identity]
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public int MtgLevel { get; set; }
    public int KdKhusus { get; set; }
    public long IdJRek { get; set; }
    public long? IdJnsAkun { get; set; }
    public string Type { get; set; }
    public int? StAktif { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
  }
}
