using Business.Dto;
using DataAccess.Models.Workers;

namespace Business.Mapping;

public static class EmployeeMapper
{
    public static EmployeeDto AsDto(this Employee employee)
        => new EmployeeDto(employee.Name, employee.Id, employee.Account.Login, employee.Group.Name);
}