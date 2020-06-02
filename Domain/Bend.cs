using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain
{
  [Table("BEND")]
  public class Bend
  {
    [Key]
    public string KeyBend { get; set; }
    public string UnitKey { get; set; }
    public string NIP { get; set; }
    [LeftJoin("PEGAWAI", "NIP", "NIP")]
    public Pegawai Pegawai { get; set; }
    public string Jns_Bend { get; set; }
    public int StAktif { get; set; }
    public string KdBank { get; set; }
    public string Jab_Bend { get; set; }
    public string RekBend { get; set; }
    public string NPWPBend { get; set; }
    public Decimal? SaldoBend { get; set; }
    public Decimal? SaldoBendT { get; set; }
    public DateTime? TglStopBend { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}