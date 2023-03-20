using DataAccess.Models.Messages;

namespace Business.Dto.MessageDtos;

public record MessengerMessageDto(Guid Recipient, string Text, DateTime SendingTime, string UserTag)
    :MessageDto(Recipient, Text, SendingTime);