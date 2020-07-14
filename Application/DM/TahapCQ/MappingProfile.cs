using AutoMapper;
using Domain.DM;

namespace Application.DM.TahapCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Tahap>();
      CreateMap<Update.Command, Tahap>();
    }
  }
}