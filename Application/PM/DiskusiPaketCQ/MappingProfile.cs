using Application.Auth.User;
using Domain.PM;
using Profile = AutoMapper.Profile;

namespace Application.PM.DiskusiPaketCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DiskusiPaket>();
      CreateMap<Update.Command, DiskusiPaket>();
    }
  }
}