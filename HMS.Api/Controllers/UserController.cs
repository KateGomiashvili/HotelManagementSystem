using HMS.Models.Entities;
using HMS.Service.Implementations;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [Authorize(Roles ="Manager,Admin")]
        [HttpDelete("manager")]
        public async Task<IActionResult> DeleteManager([FromForm]string managerId)
        {
           await _usersService.DeleteManager(managerId);
            ApiResponse response = new(ApiResponseMessage.successMessage, managerId, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("guest")]
        public async Task<IActionResult> DeleteGuest([FromForm] string guestId)
        {
            await _usersService.DeleteGuest(guestId);
            ApiResponse response = new(ApiResponseMessage.successMessage, guestId, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
    }
}
