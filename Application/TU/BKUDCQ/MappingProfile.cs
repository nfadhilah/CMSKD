using AutoMapper;
using Domain.TU;

namespace Application.TU.BKUDCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BKUD>();
    }
  }
}
