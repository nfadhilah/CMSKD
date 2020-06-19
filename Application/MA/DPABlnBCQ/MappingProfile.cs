using AutoMapper;
using Domain.MA;

namespace Application.MA.DPABlnBCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPABlnB>();
      CreateMap<Update.Command, DPABlnB>();
    }
  }
}
