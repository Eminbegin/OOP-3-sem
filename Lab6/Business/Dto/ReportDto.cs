using DataAccess.Models;

namespace Business.Dto;

public record ReportDto(Guid Id, int MessagesCount, int HandledMessagesCount, DateTime Date, IReadOnlyCollection<SendMethodMessages> MethodMessages);