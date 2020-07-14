using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPBPKCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Berita>();
      CreateMap<Update.Command, Berita>();
      CreateMap<SPPBPK, SPPBPKDTO>()
        .ForMember(s => s.NoSPP, opt => opt.MapFrom(s => s.SPP.NoSPP.Trim()))
        .ForMember(s => s.NoBPK,
          opt => opt.MapFrom(s => s.BPK.NoBPK.Trim()));
    }
  }
}
