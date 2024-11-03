using AutoMapper;
using BankSystem.Api.Services.Interfaces;
using BankSystem.Application.Dto.EmployeeDto;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.Api.Services.Implementations;

public class EmployeeService : BaseService<Employee, EmployeeResponse>, IEmployeeService
{
    private readonly IEmployeeStorage _employeeStorage;

    public EmployeeService(IEmployeeStorage employeeStorage, IMapper mapper)
        : base(employeeStorage, mapper)
    {
        _employeeStorage = employeeStorage;
    }
}