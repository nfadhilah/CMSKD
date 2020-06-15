using AutoMapper;
using Domain;

namespace Application.JabTandaTangan
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
