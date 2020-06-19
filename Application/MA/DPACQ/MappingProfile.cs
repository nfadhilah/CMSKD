using AutoMapper;
using Domain.MA;

namespace Application.MA.DPACQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPA>();
      CreateMap<Update.Command, DPA>();
    }
  }
}
