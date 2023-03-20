using Business.Dto.MessageDtos;
using DataAccess.Models.Messages;

namespace Business.AbstractFactories.ImplementationMessageFactory;

public class PhoneMessageFactory : IMessageFactory

{
    private IMessageFactory? _spareFactory;
    
    public AbstractMessage CreateMessage(MessageDto messageDto)
    {
        if (messageDto is PhoneMessageDto phone)
        {
            return new PhoneMessage(Guid.NewGuid(), phone.Text, MessageState.New, phone.Recipient, DateTime.Now, phone.PhoneNumber);
        }

        if (_spareFactory is not null)
        {
            return _spareFactory.CreateMessage(messageDto);
        }

        throw new Exception();
    }

    public IMessageFactory AddSpareFactory(IMessageFactory messageFactory)
    {
        ArgumentNullException.ThrowIfNull(messageFactory);
        _spareFactory = messageFactory;
        return _spareFactory;
    }
}