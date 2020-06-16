using System.Threading.Tasks;
using Application.DM.StatusTransaksi;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
    public class StatTrsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] List.Query query) =>
          Ok(await Mediator.Send(query));

        [HttpGet("{id}", Name = "GetKodeStatTrs")]
        public async Task<IActionResult> Get(long id) =>
          Ok(await Mediator.Send(new Detail.Query { IdStatTrs = id }));

        [HttpPost]
        public async Task<IActionResult> Create(Create.Command command)
        {
            var request = await Mediator.Send(command);
            return CreatedAtRoute("GetKodeStatTrs", new { id = request.IdStatTrs }, request);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Update.Command command)
        {
            command.IdStatTrs = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) =>
          Ok(await Mediator.Send(new Delete.Command { IdStatTrs = id }));
    }
}
