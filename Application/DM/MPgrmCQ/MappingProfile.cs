using AutoMapper;
using Domain.DM;

namespace Application.DM.MPgrmCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, MPgrm>();
      CreateMap<Update.Command, MPgrm>();
    }
  }
}
