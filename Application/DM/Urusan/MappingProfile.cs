using AutoMapper;
using Domain.DM;

namespace Application.DM.Urusan
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
