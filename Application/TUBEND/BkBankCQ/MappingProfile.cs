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
      CreateMap<BkBank, BKBankDTO>()
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()));
    }
  }
}
