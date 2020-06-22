using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPKDetR>();
      CreateMap<Update.Command, BPKDetR>();
    }
  }
}
