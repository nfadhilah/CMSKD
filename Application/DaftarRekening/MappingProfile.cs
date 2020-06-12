using AutoMapper;
using Domain;

namespace Application.DaftarRekening
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftRekening>();
      CreateMap<Update.Command, DaftRekening>();
    }
  }
}
