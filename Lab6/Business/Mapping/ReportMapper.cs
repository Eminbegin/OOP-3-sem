using Business.Dto;
using DataAccess.Models;

namespace Business.Mapping;

public static class ReportMapper
{
    public static ReportDto AsDto(this Report report)
        => new ReportDto(report.Id, report.Statistic.MessagesCount, report.Statistic.HandledMessagesCount, report.Statistic.Date, report.Statistic.MethodMessages.ToList());
}