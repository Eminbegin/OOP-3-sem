using Business.Dto;
using DataAccess.Models;

namespace Business.Mapping;

public static class AreaMapper
{
    public static AreaDto AsDto(this Area area)
        => new AreaDto(area.Name, area.Id);
}