using AutoMapper;
using Domain.DM;

namespace Application.DM.DaftarRekening
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftRekening>();
      CreateMap<Update.Command, DaftRekening>();
    }
  }
}
