using Business.Dto;

namespace Business.Services;

public interface ISessionService
{
    Task<SessionDto> CreateSessionAsync(Guid login, Guid password, CancellationToken cancellationToken);
    Task<bool>  RemoveSessionAsync(Guid login, CancellationToken cancellationToken);

}