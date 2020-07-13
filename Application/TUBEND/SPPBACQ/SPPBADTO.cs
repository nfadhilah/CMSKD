using System;

namespace Application.TUBEND.SPPBACQ
{
  public class SPPBADTO
  {
    public long IdSPPBA { get; set; }
    public long IdSPP { get; set; }
    public string NoSPP { get; set; }
    public long IdBerita { get; set; }
    public string NoBerita { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
