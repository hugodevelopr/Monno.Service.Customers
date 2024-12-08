using AutoMapper;
using Monno.AppService.Responses.Customers;
using Monno.Core.Entities.Customers;

namespace Monno.AppService.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CreateCustomerResponse>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));
    }
}