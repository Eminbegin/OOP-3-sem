using Business.Dto.SendingMethods;

namespace Business.Services;

public interface ISendingMethodsService
{
    Task<SendingMethodDto> CreateSendMessageAsync(SendingMethodDto messageDto, CancellationToken cancellationToken);
    Task<GroupMethodsDto> AddMethodToGroup(Guid sessionId, Guid groupId, Guid sendingMethodId, CancellationToken cancellationToken);
    Task<WorkerMethodsDto> AddMethodToWorker(Guid sessionId, Guid sendingMethodId, CancellationToken cancellationToken);
}