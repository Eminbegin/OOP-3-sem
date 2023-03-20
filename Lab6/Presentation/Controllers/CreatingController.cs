using Business.Dto;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;


namespace Presentation.Controllers;

[ApiController]
public class CreatingController : Controller
{ 
    private readonly ICreatingService _service;

    public CreatingController(ICreatingService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/CreateArea")]
    public async Task<ActionResult<AreaDto>> CreateAreaAsync([FromBody] AreaModel model)
    {
       AreaDto areaDto = await _service.CreateAreaAsync(model.Name, model.FirstDaddyName, CancellationToken);
       return Ok(areaDto);
    }
    
    [HttpPost]
    [Route("api/CreateGroup")]
    public async Task<ActionResult<GroupDto>> CreateGroupAsync([FromBody] HighAccessModel model)
    {
        GroupDto groupDto = await _service.CreateGroupAsync(model.SessionId, model.Name, CancellationToken);
        return Ok(groupDto);
    }
    
    [HttpPost]
    [Route("api/AddDaddyToArea")]
    public async Task<ActionResult<DaddyDto>> AddDaddyToAreaAsync([FromBody] HighAccessModel model)
    {
        DaddyDto daddyDto = await _service.AddDaddyToAreaAsync(model.SessionId, model.Name, CancellationToken);
        return Ok(daddyDto);
    }
    
    [HttpPost]
    [Route("api/AddEmployeeToGroup")]
    public async Task<ActionResult<EmployeeDto>> AddEmployeeToGroup([FromBody] HighAccessModel model)
    {
        EmployeeDto employeeDto = await _service.AddEmployeeToGroup(model.SessionId, model.Name, CancellationToken);
        return Ok(employeeDto);
    }
    
    [HttpPost]
    [Route("api/AddDaddyToEmployee")]
    public async Task<ActionResult<EmployeeDto>> AddDaddyToGroup([FromBody] LowAccessModel model)
    {
        EmployeeDto employeeDto = await _service.AddDaddyToGroup(model.SessionId, model.GroupId, model.Name, CancellationToken);
        return Ok(employeeDto);
    }
    
    [HttpPost]
    [Route("api/AddEmployeeInGroup")]
    public async Task<ActionResult<EmployeeDto>> AddEmployeeToGroupByDaddy([FromBody] LowAccessModel model)
    {
        EmployeeDto employeeDto = await _service.AddEmployeeToGroupByDaddy(model.SessionId, model.GroupId, model.Name, CancellationToken);
        return Ok(employeeDto);
    }
}