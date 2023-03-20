using Business.Dto;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Models.Messages;

namespace Presentation.Controllers;

[ApiController]
public class AccountController : Controller
{
    private readonly ISessionService _service;

    public AccountController(ISessionService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/Login")]
    public async Task<ActionResult<SessionDto>> CreateSessionAsync([FromBody] AuthenticationModel model)
    {
        SessionDto session = await _service.CreateSessionAsync(model.Login, model.Password, CancellationToken);
        return Ok(session);
    }
    
    [HttpPost]
    [Route("api/Logout")]
    public async Task<ActionResult<bool>> DeleteSessionAsync([FromBody] RemoveSessionModel model)
    {
        await _service.RemoveSessionAsync(model.Login, CancellationToken);
        return Ok();
    }
}