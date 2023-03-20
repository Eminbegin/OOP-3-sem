using Business.Dto.SendingMethods;
using DataAccess.Models;
using DataAccess.Models.SendingMethods;

namespace Business.Mapping;

public static class GroupMethodsMapper
{
    public static GroupMethodsDto AsDtoWithMethods(this Group group)
    {
        return new GroupMethodsDto(group.Id, group.Name, group.SendingMethods.Select(s => s.AsDto()).ToList());
    }
}