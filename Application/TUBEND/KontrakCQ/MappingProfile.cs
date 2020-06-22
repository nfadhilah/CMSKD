using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.KontrakCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Kontrak>();
      CreateMap<Update.Command, Kontrak>();
    }
  }
}
