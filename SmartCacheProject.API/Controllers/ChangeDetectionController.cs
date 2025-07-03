using Microsoft.AspNetCore.Mvc;
using SmartCacheProject.Application.Services.Interfaces;
using SmartCacheProject.Domain.Dtos.ChangeDetection;

namespace SmartCacheProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChangeDetectionController : ControllerBase
{
    private readonly IChangeDetectionService _changeDetectionService;

    public ChangeDetectionController(IChangeDetectionService changeDetectionService)
    {
        _changeDetectionService = changeDetectionService;
    }
    [HttpPost("check-changes")]
    public async Task<IActionResult> CheckChangesAsync([FromBody] ChangeCheckRequestDto request)
    {
        if (request == null)
            return BadRequest("Request cannot be null.");
        var response = await _changeDetectionService.CheckChangesAsync(request);
        return Ok(response);
    }
}
