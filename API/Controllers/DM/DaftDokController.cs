using Application.DM.DaftDokCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DM
{
  public class DaftDokController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));


    [HttpGet("{id}", Name = "GetDaftDok")]
    public async Task<IActionResult> Get(string id) => Ok(await Mediator.Send(new Detail.Query { KdDok = id }));
  }
}