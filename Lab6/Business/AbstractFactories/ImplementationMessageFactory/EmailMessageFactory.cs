using Business.Dto.MessageDtos;
using DataAccess.Models.Messages;

namespace Business.AbstractFactories.ImplementationMessageFactory;

public class EmailMessageFactory : IMessageFactory
{
    private IMessageFactory? _spareFactory;
    
    public AbstractMessage CreateMessage(MessageDto messageDto)
    {
        if (messageDto is EmailMessageDto email)
        {
            return new EmailMessage(Guid.NewGuid(), email.Text, MessageState.New, email.Recipient, DateTime.Now, email.Theme);
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