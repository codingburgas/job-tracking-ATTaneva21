using JobTracking.Application.Contracts;
using JobTracking.Domain.DTOs;
using JobTracking.Domain.Filters;
using JobTracking.Domain.Filters.Base;
using Microsoft.AspNetCore.Mvc;
using Filters_CandidateFilter = JobTracking.Domain.Filters.CandidateFilter;

namespace JobTracking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userService.GetUserAsync(id);
            
            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving the user");
        }
    }

   
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving users");
        }
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> GetFilteredUsers([FromBody] BaseFilter<Filters_CandidateFilter> userFilter)
    {
        try
        {
            if (userFilter == null)
            {
                return BadRequest("Filter criteria cannot be null");
            }

            var users = await _userService.GetFilteredUsersAsync(userFilter);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while filtering users");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, "An error occurred while creating the user");
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userService.UpdateUserAsync(id, userDto);
            
            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the user");
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            var result = await _userService.DeleteUserAsync(id);
            
            if (!result)
            {
                return NotFound($"User with ID {id} not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deleting the user");
        }
    }
}