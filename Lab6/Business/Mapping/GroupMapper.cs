using Business.Dto;
using DataAccess.Models;

namespace Business.Mapping;

public static class GroupMapper
{
    public static GroupDto AsDto(this Group group)
        => new GroupDto(group.Name, group.Id);
}