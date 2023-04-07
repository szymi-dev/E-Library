using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikeController : BaseController
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        public LikeController(ILikeRepository likeRepository, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet("GetUserFavorites")]
        public async Task<ActionResult<List<BookDto>>> GetUserLikedBooks()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _likeRepository.GetUserFavoriteBooks(username);
        }

        [HttpPost("AddBookToFavorites/{bookId}")]
        public async Task<ActionResult> AddBookToFavorites(int bookId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // var user = await _userRepository.GetUserByUsername(username);
            try
            {
                await _likeRepository.AddBookToFavorites(bookId, username);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteBookFromFavorites/{bookId}")]
        public async Task<ActionResult> RemoveBookFromFavorites(int bookId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await _likeRepository.RemoveBookFromFavorites(bookId, username);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}