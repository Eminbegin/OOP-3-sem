using Business.Dto;
using Business.Dto.MessageDtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Messages;

namespace Presentation.Controllers;

[ApiController]
public class MessageSendingController : Controller
{
    private readonly IMessageService _service;

    public MessageSendingController(IMessageService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/SendEmailMessage")]
    public async Task<ActionResult<SessionDto>> SendEmailMessageAsync([FromBody] EmailMessageModel model)
    {
        MessageDto message = await _service.SendMessageAsync(new EmailMessageDto(model.Recipient, model.Text, DateTime.MinValue, model.Theme), CancellationToken);
        return Ok(message);
    }
    
    [HttpPost]
    [Route("api/SendPhoneMessage")]
    public async Task<ActionResult<SessionDto>> SendPhoneMessageAsync([FromBody] PhoneMessageModel model)
    {
        MessageDto message = await _service.SendMessageAsync(new PhoneMessageDto(model.Recipient, model.Text, DateTime.MinValue, model.PhoneNumber), CancellationToken);
        return Ok(message);
    }
    
    [HttpPost]
    [Route("api/SendMessengerMessage")]
    public async Task<ActionResult<SessionDto>> SendMessengerMessageAsync([FromBody] MessengerMessageModel model)
    {
        MessageDto message = await _service.SendMessageAsync(new MessengerMessageDto(model.Recipient, model.Text, DateTime.MinValue, model.UserTag), CancellationToken);
        return Ok(message);
    }
}
