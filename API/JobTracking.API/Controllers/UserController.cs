using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

    /// <summary>
    /// Gets a user by ID
    /// </summary>
    /// <param name="id">The user ID</param>
    /// <returns>The user details</returns>
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
            // Log the exception here
            return StatusCode(500, "An error occurred while retrieving the user");
        }
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <returns>List of all users</returns>
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
            // Log the exception here
            return StatusCode(500, "An error occurred while retrieving users");
        }
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> GetFilteredUsers([FromBody] BaseFilter<UserFilter> userFilter)
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
            // Log the exception here
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
            // Log the exception here
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
            // Log the exception here
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
            // Log the exception here
            return StatusCode(500, "An error occurred while deleting the user");
        }
    }
}