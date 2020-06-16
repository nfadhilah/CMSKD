using AutoMapper;
using Domain.DM;

namespace Application.DM.SifatKegiatan
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
