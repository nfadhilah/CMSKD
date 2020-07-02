using AutoMapper;
using Domain.MA;

namespace Application.MA.DPABCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DPAB>();
      CreateMap<Update.Command, DPAB>();
      CreateMap<DPAB, DPABDTO>()
        .ForMember(d => d.NoDPA, opt => opt.MapFrom(s => s.DPA.NoDPA.Trim()))
        .ForMember(d => d.KdPer, opt => opt.MapFrom(s => s.DaftRekening.KdPer.Trim()))
        .ForMember(d => d.NmPer, opt => opt.MapFrom(s => s.DaftRekening.NmPer.Trim()));
    }
  }
}
