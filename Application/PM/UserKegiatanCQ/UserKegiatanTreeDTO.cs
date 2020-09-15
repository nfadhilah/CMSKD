namespace Application.PM.UserKegiatanCQ
{
  public class UserKegiatanTreeDTO
  {
    public int Lvl { get; set; }
    public string Kode { get; set; }
    public string Label { get; set; }
    public long? IdKeg { get; set; }
    public string UserId { get; set; }
    public string PPK { get; set; }
    public int? IsSelected { get; set; }
    public string Type { get; set; }
  }
}