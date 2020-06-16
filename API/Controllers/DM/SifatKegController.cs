using System.Threading.Tasks;
using Application.DM.SifatKegiatan;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
    public class SifatKegController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] List.Query query) =>
          Ok(await Mediator.Send(query));

        [HttpGet("{id}", Name = "GetKodeSifatKegiatan")]
        public async Task<IActionResult> Get(long id) =>
          Ok(await Mediator.Send(new Detail.Query { IdSifatKeg = id }));

        [HttpPost]
        public async Task<IActionResult> Create(Create.Command command)
        {
            var request = await Mediator.Send(command);
            return CreatedAtRoute("GetKodeSifatKegiatan", new { id = request.IdSifatKeg }, request);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Update.DTO dto)
        {
            var command = dto.MapDTO(new Update.Command());
            command.IdSifatKeg = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) =>
          Ok(await Mediator.Send(new Delete.Command { IdSifatKeg = id }));
    }
}
