using AutoMapper;
using Domain.DM;

namespace Application.DM.JAKasCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JAKas>();
      CreateMap<Update.Command, JAKas>();
    }
  }
}
