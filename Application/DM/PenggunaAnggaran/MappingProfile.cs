using AutoMapper;
using Domain.DM;

namespace Application.DM.PenggunaAnggaran
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
