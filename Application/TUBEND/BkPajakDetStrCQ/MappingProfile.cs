using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BkPajakDetStrCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BkPajakDetStr>();
      CreateMap<Update.Command, BkPajakDetStr>();
    }
  }
}
