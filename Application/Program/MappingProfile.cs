using AutoMapper;
using Domain;

namespace Application.Program
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, MPgrm>();
      CreateMap<Update.Command, MPgrm>();
    }
  }
}
