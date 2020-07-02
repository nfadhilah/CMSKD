using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("KegUnit")]
  public class KegUnit
  {
    [Key, Identity]
    public long IdKegUnit { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public long IdKeg { get; set; }
    [LeftJoin("MKEGIATAN", "IDKEG", "IDKEG")]
    public MKegiatan MKegiatan { get; set; }
    public string KdTahap { get; set; }
    public long IdPrgrm { get; set; }
    [LeftJoin("MPGRM", "IDPRGRM", "IDPRGRM")]
    public MPgrm MPgrm { get; set; }
    public int? NoPrior { get; set; }
    public long IdSifatKeg { get; set; }
    public long? IdPeg { get; set; }
    [LeftJoin("PEGAWAI", "IDPEG", "IDPEG")]
    public Pegawai Pegawai { get; set; }
    public DateTime? TglAkhir { get; set; }
    public DateTime? TglAwal { get; set; }
    public decimal? TargetP { get; set; }
    public string Lokasi { get; set; }
    public decimal? JumlahMin1 { get; set; }
    public decimal? Pagu { get; set; }
    public decimal? JumlahPls1 { get; set; }
    public string Sasaran { get; set; }
    public string KetKeg { get; set; }
    public string IdPrioDa { get; set; }
    public string IdSas { get; set; }
    public string Target { get; set; }
    public string TargetIf { get; set; }
    public string TargetSen { get; set; }
    public string Volume { get; set; }
    public string Volume1 { get; set; }
    public string Satuan { get; set; }
    public decimal? PaguPlus { get; set; }
    public decimal? PaguTif { get; set; }
    public DateTime? TglValid { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}