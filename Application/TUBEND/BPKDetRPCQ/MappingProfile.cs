using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKDetRPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPKDetRP>();
      CreateMap<Update.Command, BPKDetRP>();
    }
  }
}
