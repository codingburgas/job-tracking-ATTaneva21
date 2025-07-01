using Microsoft.AspNetCore.Mvc;
using JobTracking.Application.Contracts;

namespace JobTracking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _service;

    public JobsController(IJobService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllJobsAsync());
    }
}