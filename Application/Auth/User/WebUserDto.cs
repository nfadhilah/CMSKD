namespace Application.Auth.User
{
  public class WebUserDto
  {
    public string UserId { get; set; }
    public long? IdUnit { get; set; }
    public int KdTahap { get; set; }
    public long? IdPeg { get; set; }
    public long GroupId { get; set; }
    public string Nama { get; set; }
    public string Email { get; set; }
    public int? BlokId { get; set; }
    public bool Transecure { get; set; }
    public bool StInsert { get; set; }
    public bool StUpdate { get; set; }
    public bool StDelete { get; set; }
    public string Ket { get; set; }
  }
}
