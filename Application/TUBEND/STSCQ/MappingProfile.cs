using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.STSCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, STS>();
      CreateMap<Update.Command, STS>();
      CreateMap<STS, STSDTO>();
    }
  }
}
