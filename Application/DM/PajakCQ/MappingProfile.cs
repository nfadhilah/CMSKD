using AutoMapper;
using Domain.DM;

namespace Application.DM.PajakCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Pajak>();
      CreateMap<Update.Command, Pajak>();
    }
  }
}
