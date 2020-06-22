using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BkBankCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BkBank>();
      CreateMap<Update.Command, BkBank>();
    }
  }
}
