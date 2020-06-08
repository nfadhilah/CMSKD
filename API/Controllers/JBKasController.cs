using Application.JenisBuktiKas;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class JBKasController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "DetailJenisBuktiKas")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdBKas = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("Detail", new { id = request.IdBKas }, request);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.Command command)
    {
      command.IdBKas = id;
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdBKas = id }));
  }
}