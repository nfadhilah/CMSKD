using AutoMapper;
using Domain;

namespace Application.SifatKegiatan
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Create.Command, SifatKeg>();
			CreateMap<Update.Command, SifatKeg>();
		}
	}
}
