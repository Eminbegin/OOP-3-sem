using Business.Dto.MessageDtos;
using DataAccess.Models.Messages;

namespace Business.AbstractFactories.ImplementationMessageFactory;

public class MessengerMessageFactory : IMessageFactory

{
    private IMessageFactory? _spareFactory;
    
    public AbstractMessage CreateMessage(MessageDto messageDto)
    {
        if (messageDto is MessengerMessageDto messenger)
        {
            return new MessengerMessage(Guid.NewGuid(), messenger.Text, MessageState.New, messenger.Recipient, DateTime.Now, messenger.UserTag);
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