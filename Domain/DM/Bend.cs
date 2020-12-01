﻿using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("BEND")]
  public class Bend
  {
    [Key, Identity]
    public long IdBend { get; set; }
    public string JnsBend { get; set; }
    public long IdPeg { get; set; }
    [InnerJoin("PEGAWAI", "IDPEG", "IDPEG")]
    public Pegawai Peg { get; set; }
    public string IdBank { get; set; }
    [InnerJoin("JBANK", "IDBANK", "IDBANK")]
    public JBank Bank { get; set; }
    public string NmCabBank { get; set; }
    public string RekBend { get; set; }
    public string NPWPBend { get; set; }
    public string JabBend { get; set; }
    public Decimal? SaldoBend { get; set; }
    public Decimal? SaldoBendT { get; set; }
    public DateTime? TglStopBend { get; set; }
    public string WargaNegara { get; set; }
    public int? StAktif { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
  }
}