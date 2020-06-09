﻿using AutoMapper;
using Domain;

namespace Application.Rekanan
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftPhk3>();
      CreateMap<Update.Command, DaftPhk3>();
    }
  }
}
