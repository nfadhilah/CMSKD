using AutoMapper;
using Domain.BUD;

namespace Application.BUD.DPDetCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPDet>();
      CreateMap<Update.Command, DPDet>();
    }
  }
}
