using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKPajakStrCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPKPajakStr>();
      CreateMap<Update.Command, BPKPajakStr>();
    }
  }
}
