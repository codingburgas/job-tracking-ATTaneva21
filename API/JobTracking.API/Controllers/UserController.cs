using Microsoft.AspNetCore.Mvc;
using JobTracking.Domain.DTOs.Response;

namespace JobTracking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _service.GetUserByIdAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.CreateUserAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var exists = await _service.UserExistsAsync(id);
        if (!exists) return NotFound();

        var updated = await _service.UpdateUserAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteUserAsync(id);
        return success ? NoContent() : NotFound();
    }
}