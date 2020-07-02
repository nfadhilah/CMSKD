using AutoMapper;
using Domain.DM;

namespace Application.DM.SifatKegCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SifatKeg>();
      CreateMap<Update.Command, SifatKeg>();
    }
  }
}
