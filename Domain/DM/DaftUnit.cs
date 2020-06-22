﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
{
  [Table("DAFTUNIT")]
  public class DaftUnit
  {
    [Key, Identity]
    public int IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public int KdLevel { get; set; }
    public string Type { get; set; }
    public string AkroUnit { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public int StAktif { get; set; }
    [UpdatedAt]
    public DateTime DateCreate { get; set; }
  }
}