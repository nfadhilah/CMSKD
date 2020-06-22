using AutoMapper;
using Domain.MA;

namespace Application.MA.SPDCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPD>();
      CreateMap<Update.Command, SPD>();
    }
  }
}
