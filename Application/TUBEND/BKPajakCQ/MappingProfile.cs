using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.BKPajakCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, BkPajak>();
      CreateMap<Update.Command, BkPajak>();
    }
  }
}
