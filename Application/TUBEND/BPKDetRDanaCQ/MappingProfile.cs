using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKDetRDanaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPKDetRDana>();
      CreateMap<Update.Command, BPKDetRDana>();
    }
  }
}
