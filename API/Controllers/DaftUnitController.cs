using Application.UnitOrganisasi;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class DaftUnitController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get() =>
      Ok(await Mediator.Send(new List.Query()));

    [HttpGet("nodes")]
    public async Task<IActionResult> GetNode([FromQuery] Node.Query query) =>
      Ok(
        await Mediator.Send(new Node.Query
        { KdLevel = query.KdLevel, KdUnit = query.KdUnit }));
  }
}