using AutoMapper;
using Domain.DM;

namespace Application.DM.JabTtdCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JabTtd>();
      CreateMap<Update.Command, JabTtd>();
    }
  }
}
