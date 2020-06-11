﻿using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("DAFTPHK3")]
  public class DaftPhk3
  {
    [Key, Identity]
    public int IdPhk3 { get; set; }
    public string NmPhk3 { get; set; }
    public string NmInst { get; set; }
    public int IdBank { get; set; }
    public string CabangBank { get; set; }
    public string AlamatBank { get; set; }
    public string NoRekBank { get; set; }
    public int IdJUsaha { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string NPWP { get; set; }
    public int StValid { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}