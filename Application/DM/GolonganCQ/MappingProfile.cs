using AutoMapper;
using Domain.DM;

namespace Application.DM.GolonganCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Golongan>();
      CreateMap<Update.Command, Golongan>();
      CreateMap<Golongan, GolonganDTO>()
        .ForMember(d => d.KdGol, opt => opt.MapFrom(s => s.KdGol.Trim()));
    }
  }
}
