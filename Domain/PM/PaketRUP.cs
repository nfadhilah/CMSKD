﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.PM
{
  [Table("PAKETRUP")]
  public class PaketRUP
  {
    [Key, Identity]
    public long IdRUP { get; set; }

    public long IdUnit { get; set; }
    public JnsRUP JnsRUP { get; set; }
    public int TipeSwakelola { get; set; }
    public string UraiTipeSwakelola { get; set; }

    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }

    public long IdKeg { get; set; }

    [InnerJoin("MKEGIATAN", "IDKEG", "IDKEG")]
    public MKegiatan Keg { get; set; }

    public decimal? NilaiPagu { get; set; }
    public DateTime? TglValid { get; set; }

    public string KodeRUP { get; set; }
    public string NmPaket { get; set; }
    public string IdLokasi { get; set; }
    public string Lokasi { get; set; }
    public string Volume { get; set; }
    public string UraiPaket { get; set; }
    public long? IdJnsPekerjaan { get; set; }
    public StatusRUP Status { get; set; }

    [LeftJoin("JPEKERJAAN", "IDJNSPEKERJAAN", "IDJNSPEKERJAAN")]
    public JPekerjaan JnsPekerjaan { get; set; }

    public long? IdMetodePengadaan { get; set; }

    [LeftJoin("METODEPENGADAAN", "IDMETODEPENGADAAN", "IDMETODEPENGADAAN")]
    public MetodePengadaan MetodePengadaan { get; set; }

    public DateTime? AwalPekerjaan { get; set; }
    public DateTime? AkhirPekerjaan { get; set; }
    public long IdJDana { get; set; }

    [InnerJoin("JDANA", "IDJDANA", "IDJDANA")]
    public JDana JDana { get; set; }

    public long? IdPhk3 { get; set; }

    [LeftJoin("DAFTPHK3", "IDPHK3", "IDPHK3")]
    public DaftPhk3 Phk3 { get; set; }

    [UpdatedAt]
    public DateTime? DateCreate { get; set; }

    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }

    public bool? A { get; set; }
    public bool? FD { get; set; }
    public bool? U { get; set; }
    public string CreatedBy { get; set; }
  }
}