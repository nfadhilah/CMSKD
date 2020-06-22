using AutoMapper;
using Domain.DM;

namespace Application.DM.ZKodeCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, ZKode>();
      CreateMap<Update.Command, ZKode>();
    }
  }
}
