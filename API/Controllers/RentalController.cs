using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RentalController : BaseController
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IUserRepository _userRepository;
        public RentalController(IRentalRepository rentalRepository, IUserRepository userRepository)
        {
            _rentalRepository = rentalRepository;
            _userRepository = userRepository;
        }

        [HttpGet("GetRentals")]
        public async Task<ActionResult<List<RentalDto>>> GetRentals()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _rentalRepository.GetRentals(username);
        }

        [HttpPost("RentBook/{bookId}")]
        public async Task<ActionResult<RentalDto>> RentBook(int bookId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var rental = await _rentalRepository.RentalBook(bookId, username);

            return rental;
        }

        [HttpDelete("ReturnBook/{bookId}")]
        public async Task<ActionResult> ReturnBook(int bookId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await _rentalRepository.ReturnBook(bookId, username);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}