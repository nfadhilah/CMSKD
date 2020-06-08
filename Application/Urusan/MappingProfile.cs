using AutoMapper;
using Domain;

namespace Application.Urusan
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, UrusanUnit>();
      CreateMap<Update.Command, UrusanUnit>();
    }
  }
}
