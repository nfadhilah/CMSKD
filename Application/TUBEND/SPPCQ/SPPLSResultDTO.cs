using Application.TUBEND.SPPBACQ;
using Application.TUBEND.SPPDetRCQ;
using System.Collections.Generic;

namespace Application.TUBEND.SPPCQ
{
  public class SPPLSResultDTO
  {
    public SPPDTO SPP { get; set; }
    public IEnumerable<SPPBADTO> SPPBAList { get; set; }
    public IEnumerable<SPPDetRDTO> SPPDetRList { get; set; }
  }
}
