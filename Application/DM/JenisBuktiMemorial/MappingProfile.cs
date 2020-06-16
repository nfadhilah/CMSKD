using AutoMapper;
using Domain.DM;

namespace Application.DM.JenisBuktiMemorial
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
