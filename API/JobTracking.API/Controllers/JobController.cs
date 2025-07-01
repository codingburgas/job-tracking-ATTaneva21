using Microsoft.AspNetCore.Mvc;
using JobTracking.Application.Contracts;
using JobTracking.Domain.DTOs;
using JobTracking.Domain.Filters;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var job = await _service.GetJobByIdAsync(id);
        if (job == null) return NotFound();
        return Ok(job);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpdateJobDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.CreateJobAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = result.JobId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateJobDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var exists = await _service.JobExistsAsync(id);
        if (!exists) return NotFound();

        var updated = await _service.UpdateJobAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteJobAsync(id);
        return success ? NoContent() : NotFound();
    }
    
    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] JobFilter filter)
    {
        var result = await _service.GetFilteredJobsAsync(filter);
        return Ok(result);
    }

}