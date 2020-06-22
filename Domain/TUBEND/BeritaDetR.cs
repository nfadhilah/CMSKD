using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BERITADETR")]
  public class BeritaDetR
  {
    [Key, Identity]
    public long IdBeritaDet { get; set; }
    public long IdBerita { get; set; }
    [InnerJoin("BERITA", "IDBERITA", "IDBERITA")]
    public Berita Berita { get; set; }
    public long IdRek { get; set; }
    [InnerJoin("DAFTREKENING", "IDREK", "IDREK")]
    public DaftRekening Rekening { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
