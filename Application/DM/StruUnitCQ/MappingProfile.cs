﻿using AutoMapper;
using Domain.DM;

namespace Application.DM.StruUnitCQ
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Create.Command, StruUnit>();
			CreateMap<Update.Command, StruUnit>();
		}
	}
}