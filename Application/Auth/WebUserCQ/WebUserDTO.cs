﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.WebUserCQ
{
  public class WebUserDTO
  {
    public string UserId { get; set; }
    public int KdTahap { get; set; }
    public string UnitKey { get; set; }
    public string GroupId { get; set; }
    public string NmGroup { get; set; }
    public string NIP { get; set; }
    public string Nama { get; set; }
    public int BlokId { get; set; }
  }
}