using Business.Dto;
using Business.Dto.MessageDtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Models.Messages;
using Presentation.Models.Reports;

namespace Presentation.Controllers;

[ApiController]
public class ReportController : Controller
{
    private readonly IReportService _service;

    public ReportController(IReportService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/CreateReport")]
    public async Task<ActionResult<SessionDto>> CreateReportAsync([FromBody] CreateReportModel model)
    {
        ReportDto report = await _service.CreateReportAsync(model.DateTime, model.SessionId, CancellationToken);
        return Ok(report);
    }
    
    [HttpPost]
    [Route("api/GetReport")]
    public async Task<ActionResult<SessionDto>> GetReportAsync([FromBody] GetReportModel model)
    {
        ReportDto report = await _service.GetReportAsync(model.Id, model.SessionId, CancellationToken);
        return Ok(report);
    }
    
    [HttpPost]
    [Route("api/GetAllReports")]
    public async Task<ActionResult<SessionDto>> SendMessengerMessageAsync([FromBody] GetAllReportModel model)
    {
        IEnumerable<ReportDto> reportDtos = await _service.GetAllReportsAsync(model.SessionId, CancellationToken);
        return Ok(reportDtos);
    }
}
