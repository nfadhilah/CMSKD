using AutoMapper;
using Domain.DM;

namespace Application.DM.DafPajak
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Pajak>();
      CreateMap<Update.Command, Pajak>();
    }
  }
}
