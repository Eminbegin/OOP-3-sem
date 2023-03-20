using Business.Dto;
using Business.Extensions;
using Business.Mapping;
using DataAccess;
using DataAccess.Models;

namespace Business.Services.Implementation;

public class SessionService : ISessionService
{
    private readonly DataContext _context;
    
    public SessionService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<SessionDto> CreateSessionAsync(Guid login, Guid password, CancellationToken cancellationToken)
    {
        Account account = await _context.Accounts.GetEntityAsync(login, cancellationToken);
        if (account.Password != password)
        {
            throw new Exception();
        }
        
        Session? findSession = _context.Sessions.FirstOrDefault(s => s.Login == login);
        if (findSession is not null)
        {
            throw new Exception();
        }

        var statistic = new Statistic(Guid.NewGuid(), 0, 0, DateTime.Now);
        var session = new Session(Guid.NewGuid(), account.Login, account.WorkerId, statistic);
    
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);
    
        return session.AsDto();
    }
    
    public async Task<bool> RemoveSessionAsync(Guid login, CancellationToken cancellationToken)
    {
        Session? session = _context.Sessions.FirstOrDefault(s => s.Login == login);
        if (session is null)
            throw new Exception();
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}