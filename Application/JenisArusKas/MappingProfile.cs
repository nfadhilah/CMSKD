using AutoMapper;
using Domain;

namespace Application.JenisArusKas
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JAKas>();
      CreateMap<Update.Command, JAKas>();
    }
  }
}
