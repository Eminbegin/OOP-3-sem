using DataAccess.Models.Messages;

namespace Business.Dto.MessageDtos;

public record MessageDto(Guid Recipient, string Text, DateTime SendingTime);