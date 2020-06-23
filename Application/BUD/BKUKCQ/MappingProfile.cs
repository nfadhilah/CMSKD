using AutoMapper;
using Domain.BUD;

namespace Application.BUD.BKUKCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BKUK>();
      CreateMap<Update.Command, BKUK>();
    }
  }
}
