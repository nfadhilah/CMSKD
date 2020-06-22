using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.STSDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, STSDetR>();
      CreateMap<Update.Command, STSDetR>();
    }
  }
}
