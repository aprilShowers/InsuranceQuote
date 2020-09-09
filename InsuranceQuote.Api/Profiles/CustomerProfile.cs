using AutoMapper;
using InsuranceQuote.Api.Data.Entities;
using InsuranceQuote.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerCreateDto, InsuranceCustomer>()
                .ReverseMap();

            CreateMap<InsuranceCustomer, CustomerQuoteReadDto>()
                .ForMember(dest => dest.Premium, opt => opt.MapFrom(src => src.Premium));
                // map the decimal in obj to int for returndto
        }
    }
}
