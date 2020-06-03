using Application.Bendahara;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class BendController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));
    // public async Task<IActionResult> Get() =>
    //   Ok(await Mediator.Send(new List.Query()));

    [HttpGet("{id}", Name = "GetBend")]
    public async Task<IActionResult> Get(string id) =>
      Ok(await Mediator.Send(new Detail.Query { KeyBend = id }));

    [HttpGet("nodes")]
    public async Task<IActionResult> GetNode([FromQuery] Node.Query query) =>
      Ok(
        await Mediator.Send(new Node.Query
        { StAktif = query.StAktif, KdBank = query.KdBank }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetBend", new { id = request.KeyBend }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Update.Command command)
    {
      command.KeyBend = id;
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) =>
      Ok(await Mediator.Send(new Delete.Command { KeyBend = id }));
  }
}