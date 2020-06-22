using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.STSDetDCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, STSDetD>();
      CreateMap<Update.Command, STSDetD>();
    }
  }
}
