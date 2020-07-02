using System;

namespace Application.MA.DPADetBCQ
{
  public class DPADetBDTO
  {
    public long IdDPADetB { get; set; }
    public long IdDPAB { get; set; }
    public string KdNilai { get; set; }
    public string KdJabar { get; set; }
    public string Uraian { get; set; }
    public decimal? JumBYek { get; set; }
    public string Satuan { get; set; }
    public decimal? Tarif { get; set; }
    public decimal? SubTotal { get; set; }
    public string Ekspresi { get; set; }
    public byte InclSubtotal { get; set; }
    public string Type { get; set; }
    public string IdStdHarga { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
