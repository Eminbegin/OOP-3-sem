using Business.Dto;
using Business.Extensions;
using DataAccess;
using DataAccess.Models;
using Business.Mapping;
using DataAccess.Models.Workers;

namespace Business.Services.Implementation;

public class CreatingService : ICreatingService
{
    private readonly DataContext _context;

    public CreatingService(DataContext context)
    {
        _context = context;
    }

    public async Task<AreaDto> CreateAreaAsync(string name, string firstDaddyName, CancellationToken cancellationToken)
    {
        if (_context.Sessions.Any())
        {
            throw new Exception();
        }

        var workerId = Guid.NewGuid();
        var login = Guid.NewGuid();
        var password = Guid.NewGuid();
        
        var area = new Area(name, Guid.NewGuid());
        var account = new Account(login, password, workerId);
        var daddy = new Daddy(workerId, firstDaddyName, account, area);
        
        area.Daddies.Add(daddy);
        _context.Accounts.Add(account);
        _context.Workers.Add(daddy);
        _context.Areas.Add(area);
        await _context.SaveChangesAsync(cancellationToken);
        return area.AsDto();
    }

    public async Task<GroupDto> CreateGroupAsync(Guid sessionId, string name, CancellationToken cancellationToken)
    {
        Guid workerId = GetWorkerId(sessionId, _context, cancellationToken).Result;
        Area? area = _context.Areas.FirstOrDefault(x => x.Daddies.Any(d => d.Id.Equals(workerId)));
        if (area is null)
        {
            throw new Exception();
        }

        var groupId = Guid.NewGuid();
        var group = new Group(area, groupId, name);
        area.Groups.Add(group);
        _context.Groups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);
        return group.AsDto();
    }

    public async Task<DaddyDto> AddDaddyToAreaAsync(Guid sessionId, string name, CancellationToken cancellationToken)
    {
        Guid workerId = GetWorkerId(sessionId, _context, cancellationToken).Result;
        Area? area = _context.Areas.FirstOrDefault(x => x.Daddies.Any(d => d.Id.Equals(workerId)));
        if (area is null)
        {
            throw new Exception();
        }

        var daddyId = Guid.NewGuid();
        var login = Guid.NewGuid();
        var password = Guid.NewGuid();

        var account = new Account(login, password, daddyId);
        var daddy = new Daddy(daddyId, name, account, area);
        area.Daddies.Add(daddy);
        _context.Workers.Add(daddy);
        _context.Accounts.Add(account);
        
        await _context.SaveChangesAsync(cancellationToken);
        return daddy.AsDto();
    }

    public async Task<EmployeeDto> AddEmployeeToGroup(Guid sessionId, string name, CancellationToken cancellationToken)
    {
        Guid workerId = GetWorkerId(sessionId, _context, cancellationToken).Result;
        Group? group = _context.Groups.FirstOrDefault(x => x.MiniDaddies.Any(d => d.Id.Equals(workerId)));
        if (group is null)
        {
            throw new Exception();
        }
        var employeeId = Guid.NewGuid();
        var login = Guid.NewGuid();
        var password = Guid.NewGuid();
        
        var account = new Account(login, password, employeeId);
        var employee = new Employee(account, employeeId, name, group);
        _context.Accounts.Add(account);
        _context.Workers.Add(employee);
        group.Employees.Add(employee);
        
        await _context.SaveChangesAsync(cancellationToken);
        return employee.AsDto();
    }

    public async Task<EmployeeDto> AddDaddyToGroup(Guid sessionId, Guid groupId, string name, CancellationToken cancellationToken)
    {
        Guid workerId = GetWorkerId(sessionId, _context, cancellationToken).Result;
        Area? area = _context.Areas.FirstOrDefault(x => x.Daddies.Any(d => d.Id.Equals(workerId)));
        Group group = await _context.Groups.GetEntityAsync(groupId, cancellationToken);
        if (area is null)
        {
            throw new Exception();
        }

        var daddyId = Guid.NewGuid();
        var login = Guid.NewGuid();
        var password = Guid.NewGuid();

        var account = new Account(login, password, daddyId);
        var daddy = new Employee(account, daddyId, name, group);
        group.MiniDaddies.Add(daddy);
        _context.Workers.Add(daddy);
        _context.Accounts.Add(account);
        
        await _context.SaveChangesAsync(cancellationToken);
        return daddy.AsDto();
    }

    public async Task<EmployeeDto> AddEmployeeToGroupByDaddy(Guid sessionId, Guid groupId, string name, CancellationToken cancellationToken)
    {
        Guid workerId = GetWorkerId(sessionId, _context, cancellationToken).Result;
        Area? area = _context.Areas.FirstOrDefault(x => x.Daddies.Any(d => d.Id.Equals(workerId)));
        Group group = await _context.Groups.GetEntityAsync(groupId, cancellationToken);
        if (area is null)
        {
            throw new Exception();
        }

        var employeeId = Guid.NewGuid();
        var login = Guid.NewGuid();
        var password = Guid.NewGuid();

        var account = new Account(login, password, employeeId);
        var employee = new Employee(account, employeeId, name, group);
        group.Employees.Add(employee);
        _context.Workers.Add(employee);
        _context.Accounts.Add(account);
        
        await _context.SaveChangesAsync(cancellationToken);
        return employee.AsDto();
    }


    private async Task<Guid> GetWorkerId(Guid sessionId, DataContext context, CancellationToken cancellationToken)
    {
        Session session = await _context.Sessions.GetEntityAsync(sessionId, cancellationToken);
        return session.WorkerId;
    }
}