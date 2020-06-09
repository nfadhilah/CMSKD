using AutoMapper;
using Domain;

namespace Application.JenisGolongan
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
