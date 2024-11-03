using BankSystem.Application.Dto.EmployeeDto;
using BankSystemDomain.Models;

namespace BankSystem.Api.Services.Interfaces
{
    public interface IEmployeeService : IBaseService<Employee, EmployeeResponse>
    {
    }
}