﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace API.Controllers
{
  [AllowAnonymous]
  public class FallbackController : Controller
  {
    public IActionResult Index() => PhysicalFile(
      Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"),
      "text/HTML");
  }
}
