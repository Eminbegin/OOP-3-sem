using Business.Dto;

namespace Business.Services;

public interface IReportService
{
    Task<ReportDto> CreateReportAsync(DateTime dateTime, Guid sessionId, CancellationToken cancellationToken);
    Task<ReportDto> GetReportAsync(Guid id, Guid sessionId, CancellationToken cancellationToken);
    Task<IEnumerable<ReportDto>> GetAllReportsAsync(Guid sessionId, CancellationToken cancellationToken);
}