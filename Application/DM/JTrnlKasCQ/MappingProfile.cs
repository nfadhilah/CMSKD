using AutoMapper;
using Domain.DM;

namespace Application.DM.JTrnlKasCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JTrnlKas>();
      CreateMap<Update.Command, JTrnlKas>();
    }
  }
}
