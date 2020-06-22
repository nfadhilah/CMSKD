using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.STSDetBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, STSDetB>();
      CreateMap<Update.Command, STSDetB>();
    }
  }
}
