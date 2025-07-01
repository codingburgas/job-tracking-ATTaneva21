using JobTracking.Application.Contracts;
using JobTracking.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JobTracking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _service;

    public ApplicationsController(IApplicationService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var app = await _service.GetByIdAsync(id);
        return app == null ? NotFound() : Ok(app);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApplicationDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateApplicationDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var exists = await _service.ExistsAsync(id);
        if (!exists) return NotFound();

        var updated = await _service.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}