using AutoMapper;
using Domain;

namespace Application.JenisBank
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBank>();
      CreateMap<Update.Command, JBank>();
    }
  }
}
