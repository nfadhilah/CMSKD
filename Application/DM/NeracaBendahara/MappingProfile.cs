using AutoMapper;
using Domain.DM;

namespace Application.DM.NeracaBendahara
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, NrcBend>();
      CreateMap<Update.Command, NrcBend>();
    }
  }
}
