using DataAccess.Models.Messages;

namespace Business.Dto.MessageDtos;

public record PhoneMessageDto(Guid Recipient, string Text, DateTime SendingTime, string PhoneNumber)
        :MessageDto(Recipient, Text, SendingTime);
