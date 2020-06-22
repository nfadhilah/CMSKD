using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TBPLDetCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, TBPLDet>();
      CreateMap<Update.Command, TBPLDet>();
    }
  }
}
