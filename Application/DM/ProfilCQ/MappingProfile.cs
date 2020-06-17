using AutoMapper;
using Domain.DM;

namespace Application.DM.ProfilCQ
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
