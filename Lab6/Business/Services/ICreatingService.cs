using Business.Dto;

namespace Business.Services;

public interface ICreatingService
{
     Task<AreaDto> CreateAreaAsync(string name, string firstDaddyName, CancellationToken cancellationToken);
     Task<GroupDto> CreateGroupAsync(Guid sessionId, string name, CancellationToken cancellationToken);
     Task<DaddyDto> AddDaddyToAreaAsync(Guid sessionId, string name, CancellationToken cancellationToken);
     Task<EmployeeDto> AddEmployeeToGroup(Guid sessionId, string name, CancellationToken cancellationToken);
     Task<EmployeeDto> AddDaddyToGroup(Guid sessionId, Guid groupId, string name, CancellationToken cancellationToken);
     Task<EmployeeDto> AddEmployeeToGroupByDaddy(Guid sessionId, Guid groupId, string name, CancellationToken cancellationToken);
}