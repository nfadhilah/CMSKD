using AutoMapper;
using Domain;

namespace Application.JenisBuktiKas
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBKas>();
      CreateMap<Update.Command, JBKas>();
    }
  }
}
