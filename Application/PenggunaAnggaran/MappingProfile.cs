using AutoMapper;
using Domain;

namespace Application.PenggunaAnggaran
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, PA>();
      CreateMap<Update.Command, PA>();
    }
  }
}
