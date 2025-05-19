using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JobTracking.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController
{
    
    public UserController: Controller
    {
        private readonly IUserService _userSerice;

        public UserController
        {
            _userSerice = userSerice;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(id)
        {
            return Ok(await _userSerice.GetUser(id));
        }
}

}