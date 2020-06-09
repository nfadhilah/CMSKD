using AutoMapper;
using Domain;

namespace Application.DaftarBank
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftBank>();
      CreateMap<Update.Command, DaftBank>();
    }
  }
}
