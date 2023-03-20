using Business.AbstractFactories;
using Business.AbstractFactories.ImplementationSenderFactory;
using Business.Dto.SendingMethods;
using Business.Extensions;
using Business.Mapping;
using DataAccess;
using DataAccess.Models;
using DataAccess.Models.SendingMethods;
using DataAccess.Models.Workers;

namespace Business.Services.Implementation;

public class SendingMethodService : ISendingMethodsService
{
    private readonly DataContext _context;
    private readonly ISenderFactory _factory;
    
    public SendingMethodService(DataContext context)
    {
        _context = context;
        _factory = new EmailSenderFactory().AddSpareFactory(new MessengerSenderFactory().AddSpareFactory(new PhoneSenderFactory()));

    }

    public async Task<SendingMethodDto> CreateSendMessageAsync(SendingMethodDto messageDto, CancellationToken cancellationToken)
    {
        SendingMethod method = _factory.CreateSender(messageDto);
        _context.SendingMethods.Add(method);
        await _context.SaveChangesAsync(cancellationToken);
        return method.AsDto();
    }

    public async Task<GroupMethodsDto> AddMethodToGroup(Guid sessionId, Guid groupId, Guid sendingMethodId, CancellationToken cancellationToken)
    {
        Guid workerId = GetWorkerId(sessionId, _context, cancellationToken).Result;
        Group group = await _context.Groups.GetEntityAsync(groupId, cancellationToken);
        SendingMethod sendingMethod = await _context.SendingMethods.GetEntityAsync(sendingMethodId, cancellationToken);
        
        if (group.MiniDaddies.All(w => w.Id != workerId))
        {
            throw new Exception();
        }

        group.SendingMethods.Add(sendingMethod);
        await _context.SaveChangesAsync(cancellationToken);
        return group.AsDtoWithMethods();
    }

    public async Task<WorkerMethodsDto> AddMethodToWorker(Guid sessionId, Guid sendingMethodId, CancellationToken cancellationToken)
    {
        Guid workerId = GetWorkerId(sessionId, _context, cancellationToken).Result;
        SendingMethod sendingMethod = await _context.SendingMethods.GetEntityAsync(sendingMethodId, cancellationToken);
        Worker worker = await _context.Workers.GetEntityAsync(workerId, cancellationToken);
            
        worker.SendingMethods.Add(sendingMethod);
        await _context.SaveChangesAsync(cancellationToken);
        return worker.AsDtoWithMethods();
    }
    
    private async Task<Guid> GetWorkerId(Guid sessionId, DataContext context, CancellationToken cancellationToken)
    {
        Session session = await _context.Sessions.GetEntityAsync(sessionId, cancellationToken);
        return session.WorkerId;
    }
}