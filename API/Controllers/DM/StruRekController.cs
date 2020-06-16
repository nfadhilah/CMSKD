using System.Threading.Tasks;
using Application.DM.StrukturRekening;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
    public class StruRekController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] List.Query query) =>
          Ok(await Mediator.Send(query));

        [HttpGet("{id}", Name = "GetKodeStruRek")]
        public async Task<IActionResult> Get(long id) =>
          Ok(await Mediator.Send(new Detail.Query { IdStruRek = id }));

        [HttpPost]
        public async Task<IActionResult> Create(Create.Command command)
        {
            var request = await Mediator.Send(command);
            return CreatedAtRoute("GetKodeStruRek", new { id = request.IdStruRek }, request);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Update.Command command)
        {
            command.IdStruRek = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) =>
          Ok(await Mediator.Send(new Delete.Command { IdStruRek = id }));
    }
}
