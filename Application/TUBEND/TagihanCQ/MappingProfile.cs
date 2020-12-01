using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.TagihanCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Tagihan>();
      CreateMap<Update.Command, Tagihan>();
      CreateMap<Tagihan, TagihanDTO>();
    }
  }
}
