using AutoMapper;
using Domain;

namespace Application.MappingKPA
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
