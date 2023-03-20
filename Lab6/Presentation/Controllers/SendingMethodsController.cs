using Business.Dto;
using Business.Dto.SendingMethods;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Models.SendingMethods;

namespace Presentation.Controllers;

[ApiController]
public class SendingMethodsController : Controller
{
    private readonly ISendingMethodsService _service;

    public SendingMethodsController(ISendingMethodsService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/CreateEmail")]
    public async Task<ActionResult<SendingMethodDto>> CreateEmailMethod([FromBody] EmailMethodModel model)
    { 
        SendingMethodDto methodDto = await _service.CreateSendMessageAsync(new EmailSenderDto(Guid.Empty, model.Name), CancellationToken); 
        return Ok(methodDto);
    }
    
    [HttpPost]
    [Route("api/CreatePhone")]
    public async Task<ActionResult<SendingMethodDto>> CreatePhoneMethod([FromBody] PhoneMethodModel model)
    {
        SendingMethodDto methodDto = await _service.CreateSendMessageAsync(new PhoneSenderDto(Guid.Empty, model.Name), CancellationToken); 
        return Ok(methodDto);
    }
    
    [HttpPost]
    [Route("api/CreateMessenger")]
    public async Task<ActionResult<SendingMethodDto>> CreateMessengerMethod([FromBody] MessengerMethodModel model)
    {
        SendingMethodDto methodDto = await _service.CreateSendMessageAsync(new MessengerSenderDto(Guid.Empty, model.Name), CancellationToken); 
        return Ok(methodDto);
    }
    
    [HttpPost]
    [Route("api/AddMethodToGroup")]
    public async Task<ActionResult<SendingMethodDto>> AddMethodToGroup([FromBody] AddMethodToGroupModel model)
    {
        GroupMethodsDto methodDto = await _service.AddMethodToGroup(model.SessionId, model.GroupId, model.MethodId, CancellationToken); 
        return Ok(methodDto);
    }
    
    [HttpPost]
    [Route("api/AddMethodToWorker")]
    public async Task<ActionResult<SendingMethodDto>> AddMethodToWorker([FromBody] AddMethodToWorkerModel model)
    {
        WorkerMethodsDto methodDto = await _service.AddMethodToWorker(model.SessionId, model.MethodId, CancellationToken);
        return Ok(methodDto);
    }
}