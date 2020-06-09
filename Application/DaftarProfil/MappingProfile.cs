using AutoMapper;
using Domain;

namespace Application.DaftarProfil
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Profil>();
      CreateMap<Update.Command, Profil>();
    }
  }
}
