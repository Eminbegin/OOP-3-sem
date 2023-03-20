using Business.Dto.MessageDtos;
using DataAccess.Models.Messages;

namespace Business.Mapping;

public static class MessageMapper
{
    public static MessageDto AsDto(this AbstractMessage message)
    {
        if (message is EmailMessage emailMessage)
        {
            return new EmailMessageDto(emailMessage.Recipient, emailMessage.Text, emailMessage.SendingTime, emailMessage.Theme);
        }
        
        if (message is MessengerMessage messengerMessage)
        {
            return new MessengerMessageDto(messengerMessage.Recipient, messengerMessage.Text, messengerMessage.SendingTime, messengerMessage.UserTag);
        }
        
        if (message is PhoneMessage phoneMessage)
        {
            return new PhoneMessageDto(phoneMessage.Recipient, phoneMessage.Text, phoneMessage.SendingTime, phoneMessage.PhoneNumber);
        }

        throw new Exception();
    }
}