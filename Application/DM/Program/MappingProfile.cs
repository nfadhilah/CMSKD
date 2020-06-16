using AutoMapper;
using Domain.DM;

namespace Application.DM.Program
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
