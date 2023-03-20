using Business.Dto;
using Business.Extensions;
using Business.Mapping;
using DataAccess;
using DataAccess.Models;

namespace Business.Services.Implementation;

public class ReportService : IReportService
{
    private readonly DataContext _context;

    public ReportService(DataContext context)
    {
        _context = context;
    }

    public async Task<ReportDto> CreateReportAsync(DateTime dateTime, Guid sessionId, CancellationToken cancellationToken)
    {
        Session session = await _context.Sessions.GetEntityAsync(sessionId, cancellationToken);
        await _context.Workers.GetEntityAsync(session.WorkerId, cancellationToken);

        var dayStatistics = _context.Statistics.Where(x => DateOnly.FromDateTime(x.Date) == DateOnly.FromDateTime(dateTime)).ToList();
        int messagesCount = dayStatistics.Sum(x => x.MessagesCount);
        int handledMessagesCount = dayStatistics.Sum(x => x.HandledMessagesCount);
        var methodMessages = dayStatistics
            .SelectMany(x => x.MethodMessages)
            .GroupBy(x => x.SendingMethod)
            .Select(x => new SendMethodMessages(Guid.NewGuid(), x.Key, x.Sum(v => v.Count)))
            .ToList();
        var report = new Report(Guid.NewGuid(),
            new Statistic(Guid.NewGuid(), messagesCount, handledMessagesCount, DateTime.Now, methodMessages));
        
        _context.Reports.Add(report);
        _context.Statistics.RemoveRange(dayStatistics);
        await _context.SaveChangesAsync(cancellationToken);
        return report.AsDto();
    }

    public async Task<ReportDto> GetReportAsync(Guid id, Guid sessionId, CancellationToken cancellationToken)
    {
        Session session = await _context.Sessions.GetEntityAsync(sessionId, cancellationToken);
        await _context.Workers.GetEntityAsync(session.WorkerId, cancellationToken);
        
        Report report = await _context.Reports.GetEntityAsync(id, cancellationToken);
        return report.AsDto();
    }

    public async Task<IEnumerable<ReportDto>> GetAllReportsAsync(Guid sessionId, CancellationToken cancellationToken)
    {
        Session session = await _context.Sessions.GetEntityAsync(sessionId, cancellationToken);
        await _context.Workers.GetEntityAsync(session.WorkerId, cancellationToken);

        return _context.Reports.ToList().Select(x => x.AsDto());
    }
}