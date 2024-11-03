using System.Linq.Expressions;
using AutoMapper;
using BankSystem.Application.Dto.EmployeeDto;
using BankSystemDomain.Models;

namespace BankSystem.Api.Mapping
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<CreateEmployeeRequest, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportDetails, opt => opt.MapFrom(src => src.PassportNumber))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract));

            CreateMap<UpdateEmployeeRequest, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportDetails, opt => opt.MapFrom(src => src.PassportNumber))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract));

            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportDetails))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract));

            CreateMap<GetEmployeeFilterRequest, Expression<Func<Employee, bool>>>()
                .ConstructUsing(src =>
                    employee =>
                    (!src.EmployeeId.HasValue || employee.Id == src.EmployeeId) &&
                    (string.IsNullOrEmpty(src.Search) ||
                     (employee.Name.Contains(src.Search) ||
                      employee.Surname.Contains(src.Search))) &&
                    (src.BirthDay == default || employee.BirthDate.Date == src.BirthDay.Date) &&
                    (src.Salary <= 0 || employee.Salary == src.Salary));
        }
    }
}
