using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TBPLCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, TBPL>();
      CreateMap<Update.Command, TBPL>();
    }
  }
}
