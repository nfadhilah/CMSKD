using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BeritaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Berita>();
      CreateMap<Update.Command, Berita>();
    }
  }
}
