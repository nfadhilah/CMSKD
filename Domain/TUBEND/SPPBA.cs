using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("SPPBA")]
  public class SPPBA
  {
    [Key, Identity]
    public long IdSPPBA { get; set; }
    [InnerJoin("SPP", "IDSPP", "IDSPP")]
    public SPP SPP { get; set; }
    public long IdSPP { get; set; }
    [InnerJoin("BERITA", "IDBERITA", "IDBERITA")]
    public Berita Berita { get; set; }
    public long IdBerita { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
  }
}
