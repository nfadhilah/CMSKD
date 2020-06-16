using AutoMapper;
using Domain.DM;

namespace Application.DM.JenisBank
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBank>();
      CreateMap<Update.Command, JBank>();
    }
  }
}
