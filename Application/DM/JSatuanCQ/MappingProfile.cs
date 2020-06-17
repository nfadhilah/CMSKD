using AutoMapper;
using Domain.DM;

namespace Application.DM.JSatuanCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JSatuan>();
      CreateMap<Update.Command, JSatuan>();
    }
  }
}
