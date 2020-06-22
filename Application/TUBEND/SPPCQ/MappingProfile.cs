using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPP>();
      CreateMap<Update.Command, SPP>();
    }
  }
}
