using Business.AbstractFactories;
using Business.AbstractFactories.ImplementationMessageFactory;
using Business.Dto.MessageDtos;
using Business.Mapping;
using DataAccess;
using DataAccess.Models.Messages;

namespace Business.Services.Implementation;

public class MessageService : IMessageService
{
    private readonly DataContext _context;
    private readonly IMessageFactory _factory;

    public MessageService(DataContext context)
    {
        _context = context;
        _factory = new EmailMessageFactory().AddSpareFactory(new MessengerMessageFactory().AddSpareFactory(new PhoneMessageFactory()));
    }

    public async Task<MessageDto> SendMessageAsync(MessageDto messageDto, CancellationToken cancellationToken)
    {
        if (_context.SendingMethods.FirstOrDefault(s => s.Id == messageDto.Recipient) is null)
        {
            throw new Exception();
        }

        AbstractMessage message = _factory.CreateMessage(messageDto);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.AsDto();
    }
}