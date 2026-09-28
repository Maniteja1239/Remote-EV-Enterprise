using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remote_EV_Enterprise.Dtos;
using RemoteEVEnterpriseModels.Models;
using RemoteEVEnterpriseModels.Request;
using RemoteEVEnterpriseModels.Response;
using RemoteEVEnterpriseService.Interfaces;

namespace Remote_EV_Enterprise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            await _accountService.Test();
            return Ok("Test completed successfully.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterRequestModel user)
        {
            long userId = await _accountService.InsertUser(user);
            if (userId <= 0)
            {
                return BadRequest("Failed to create account.");
            }
            return Ok(userId);
        }

        [HttpGet("getUserByPhone")]
        public async Task<IActionResult> GetUserByPhone(string phoneNumber)
        {
            var user = await _accountService.GetUserByPhone(phoneNumber);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("getUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _accountService.GetUserById(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        
    }
}
