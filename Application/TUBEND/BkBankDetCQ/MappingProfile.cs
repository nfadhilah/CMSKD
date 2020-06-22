using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BkBankDetCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BkBankDet>();
      CreateMap<Update.Command, BkBankDet>();
    }
  }
}
