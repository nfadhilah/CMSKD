using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BeritaDetRCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BeritaDetR>();
      CreateMap<Update.Command, BeritaDetR>();
    }
  }
}
