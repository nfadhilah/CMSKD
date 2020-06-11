using AutoMapper;
using Domain;

namespace Application.JenisBuktiMemorial
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBM>();
      CreateMap<Update.Command, JBM>();
    }
  }
}
