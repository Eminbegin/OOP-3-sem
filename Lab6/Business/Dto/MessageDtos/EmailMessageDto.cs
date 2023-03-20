using DataAccess.Models.Messages;

namespace Business.Dto.MessageDtos;

public record EmailMessageDto(Guid Recipient, string Text, DateTime SendingTime, string Theme)
    :MessageDto(Recipient, Text, SendingTime);