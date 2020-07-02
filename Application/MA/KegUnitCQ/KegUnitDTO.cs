using System;

namespace Application.MA.KegUnitCQ
{
  public class KegUnitDTO
  {
    public long IdKegUnit { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public string KdTahap { get; set; }
    public long IdPrgrm { get; set; }
    public string NuPrgrm { get; set; }
    public string NmPrgrm { get; set; }
    public int? NoPrior { get; set; }
    public long IdSifatKeg { get; set; }
    public long? IdPeg { get; set; }
    public string NIP { get; set; }
    public string Nama { get; set; }
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
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
