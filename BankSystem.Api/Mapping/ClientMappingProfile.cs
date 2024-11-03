using System.Linq.Expressions;
using AutoMapper;
using BankSystem.Api.Dto.ClientDto;
using BankSystemDomain.Models;

namespace BankSystem.Api.Mapping
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<GetClientFilterRequest, Expression<Func<Client, bool>>>()
               .ConvertUsing((request, _) =>
                   x => (request.ClientId == null || x.Id == request.ClientId) &&
                        (string.IsNullOrEmpty(request.Search) ||
                        (x.Name + " " + x.Surname + " " + x.PassportDetails + " " + x.PhoneNumber).Contains(request.Search)) &&
                        (request.BirthDay == null || x.BirthDate == request.BirthDay));

            CreateMap<CreateClientRequest, Client>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportDetails, opt => opt.MapFrom(src => src.PassportNumber));

            CreateMap<UpdateClientRequest, Client>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportDetails, opt => opt.MapFrom(src => src.PassportNumber));

            CreateMap<Client, ClientResponse>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportDetails));
        }
    }
}
