using Business.Dto.SendingMethods;
using DataAccess.Models.SendingMethods;

namespace Business.Mapping;

public static class SendingMethodMapper
{
    public static SendingMethodDto AsDto(this SendingMethod message)
    {
        if (message is EmailSender emailSender)
        {
            return new EmailSenderDto(emailSender.Id, emailSender.Name);
        }

        if (message is PhoneSender phoneSender)
        {
            return new PhoneSenderDto(phoneSender.Id, phoneSender.Name);
        }
        
        if (message is MessengerSender messengerSender)
        {
            return new MessengerSenderDto(messengerSender.Id, messengerSender.Name);
        }

        throw new Exception();
    }
}