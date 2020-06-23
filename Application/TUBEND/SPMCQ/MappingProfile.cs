using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPMCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, SPM>();
      CreateMap<Update.Command, SPM>();
    }
  }
}
