using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepo.GetUser(id);

            return user;
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            var user = await _userRepo.GetUserByUsername(username);

            return user;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _userRepo.GetUsers();

            return users;
        }

        [HttpGet("GetFee/{userId}")]
        public ActionResult<decimal> GetUserFee(int userId)
        {
            var fee = _userRepo.GetFee(userId);
            return Ok(fee);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _userRepo.UpdateAsync(user);
            return NoContent();
        }

        [HttpPut("ResetFee/{id}")]
        public async Task<IActionResult> ResetFee(int id)
        {
            var user = await _userRepo.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepo.ResetFeeAsync(user);
            return NoContent();
        }
    }
}