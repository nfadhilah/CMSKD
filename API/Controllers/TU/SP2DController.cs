using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.TU.SP2DCQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TU
{
  public class SP2DController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = nameof(GetSP2DByNo))]
    public async Task<IActionResult> GetSP2DByNo(string id) => Ok(await Mediator.Send(new Detail.Query {NoSP2D = id}));
  }
}