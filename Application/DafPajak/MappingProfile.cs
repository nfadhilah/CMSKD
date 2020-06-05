using AutoMapper;
using Domain;

namespace Application.DafPajak
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Pajak>();
      CreateMap<Update.Command, Pajak>();
    }
  }
}
