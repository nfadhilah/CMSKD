using System.Threading.Tasks;
using Application.DM.GolonganCQ;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
    public class GolonganController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] List.Query query) =>
          Ok(await Mediator.Send(query));

        [HttpGet("{id}", Name = "GetKodeGolongan")]
        public async Task<IActionResult> Get(long id) =>
          Ok(await Mediator.Send(new Detail.Query { IdGol = id }));

        [HttpPost]
        public async Task<IActionResult> Create(Create.Command command)
        {
            var request = await Mediator.Send(command);
            return CreatedAtRoute("GetKodeGolongan", new { id = request.IdGol }, request);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Update.Command command)
        {
            command.IdGol = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) =>
          Ok(await Mediator.Send(new Delete.Command { IdGol = id }));
    }
}
