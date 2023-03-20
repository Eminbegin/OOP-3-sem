using Business.Dto.MessageDtos;
using DataAccess.Models.Messages;

namespace Business.Services;

public interface IMessageService
{
    Task<MessageDto> SendMessageAsync(MessageDto messageDto, CancellationToken cancellationToken);
}