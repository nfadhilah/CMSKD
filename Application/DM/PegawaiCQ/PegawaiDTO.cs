using System;

namespace Application.DM.PegawaiCQ
{
  public class PegawaiDTO
  {
    public long IdPeg { get; set; }
    public string NIP { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string KdGol { get; set; }
    public string NmGol { get; set; }
    public string Pangkat { get; set; }
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public string Jabatan { get; set; }
    public string PDDK { get; set; }
    public string NPWP { get; set; }
    public int StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
