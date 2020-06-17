using AutoMapper;
using Domain.DM;

namespace Application.DM.DaftBankCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftBank>();
      CreateMap<Update.Command, DaftBank>();
    }
  }
}
