﻿using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JBANK")]
  public class JBank
  {
    [Key, Identity]
    public long IdBank { get; set; }
    public string KdBank { get; set; }
    public string NmBank { get; set; }
    public string Uraian { get; set; }
    public string Akronim { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}