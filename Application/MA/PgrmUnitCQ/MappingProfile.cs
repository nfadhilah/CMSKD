using AutoMapper;
using Domain.MA;

namespace Application.MA.PgrmUnitCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, PgrmUnit>();
      CreateMap<Update.Command, PgrmUnit>();
    }
  }
}
