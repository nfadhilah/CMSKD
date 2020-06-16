using AutoMapper;
using Domain.DM;

namespace Application.DM.MappingKPA
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, KPA>();
      CreateMap<Update.Command, UrusanUnit>();
    }
  }
}
