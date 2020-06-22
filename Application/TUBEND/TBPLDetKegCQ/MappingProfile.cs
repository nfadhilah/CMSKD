using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TBPLDetKegCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, TBPLDetKeg>();
      CreateMap<Update.Command, TBPLDetKeg>();
    }
  }
}
