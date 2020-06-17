using AutoMapper;
using Domain.DM;

namespace Application.DM.JBMCQ
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
