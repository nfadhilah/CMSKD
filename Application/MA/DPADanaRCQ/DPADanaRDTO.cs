﻿using System;

namespace Application.MA.DPADanaRCQ
{
  public class DPADanaRDTO
  {
    public long IdDPADanaR { get; set; }
    public long IdDPAR { get; set; }
    public long IdJDana { get; set; }
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
