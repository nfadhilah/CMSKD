using AutoMapper;
using Domain.DM;

namespace Application.DM.JnsAkunCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JnsAkun>();
      CreateMap<Update.Command, JnsAkun>();
    }
  }
}
