using AutoMapper;
using Domain.DM;

namespace Application.DM.StatusTransaksi
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Create.Command, StatTrs>();
			CreateMap<Update.Command, StatTrs>();
		}
	}
}
