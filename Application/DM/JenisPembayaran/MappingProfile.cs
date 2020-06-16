using AutoMapper;
using Domain.DM;

namespace Application.DM.JenisPembayaran
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBayar>();
      CreateMap<Update.Command, JBayar>();
    }
  }
}
