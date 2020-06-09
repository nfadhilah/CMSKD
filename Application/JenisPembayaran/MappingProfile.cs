﻿using AutoMapper;
using Domain;

namespace Application.JenisPembayaran
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
