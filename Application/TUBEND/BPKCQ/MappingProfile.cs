using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BPKCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BPK>();
      CreateMap<Update.Command, BPK>();
    }
  }
}
