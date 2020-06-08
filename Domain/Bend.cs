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
    public string IdBend { get; set; }
    public string IdUnit { get; set; }
    public string Jns_Bend { get; set; }
    public string IdPeg { get; set; }
    [LeftJoin("PEGAWAI", "IDPEG", "IDPEG")]
    public Pegawai Pegawai { get; set; }
    public string IdBank { get; set; }
    public string RekBend { get; set; }
    public string NPWPBend { get; set; }
    public string Jab_Bend { get; set; }
    public Decimal? SaldoBend { get; set; }
    public Decimal? SaldoBendT { get; set; }
    public DateTime? TglStopBend { get; set; }
    public int StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}