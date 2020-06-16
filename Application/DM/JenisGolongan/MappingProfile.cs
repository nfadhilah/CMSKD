using AutoMapper;
using Domain.DM;

namespace Application.DM.JenisGolongan
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Golongan>();
            CreateMap<Update.Command, Golongan>();
        }
    }
}
