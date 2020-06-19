using AutoMapper;
using Domain.MA;

namespace Application.MA.DPABCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPAB>();
      CreateMap<Update.Command, DPAB>();
    }
  }
}
