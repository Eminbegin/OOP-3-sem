using Business.Dto.MessageDtos;
using DataAccess;
using DataAccess.Models.Messages;

namespace Business.AbstractFactories;

public interface IMessageFactory
{
    AbstractMessage CreateMessage(MessageDto messageDto);
    IMessageFactory AddSpareFactory(IMessageFactory messageFactory);
}
