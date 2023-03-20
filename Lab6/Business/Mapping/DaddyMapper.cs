using Business.Dto;
using DataAccess.Models;
using DataAccess.Models.Workers;

namespace Business.Mapping;

public static class DaddyMapper
{
    public static DaddyDto AsDto(this Daddy daddy)
        => new DaddyDto(daddy.Name, daddy.Id, daddy.Account.Login, daddy.Area.Name);
}