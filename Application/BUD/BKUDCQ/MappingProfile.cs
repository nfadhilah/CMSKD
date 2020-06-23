using AutoMapper;
using Domain.BUD;

namespace Application.BUD.BKUDCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BKUD>();
      CreateMap<Update.Command, BKUD>();
    }
  }
}
