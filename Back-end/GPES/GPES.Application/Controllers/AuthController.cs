using GPES.Domain.DTO;
using GPES.Domain.Services.IServices;
using GPES.Infrastructure.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GPES.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJWTService _jwtService;

        public AuthController(IUserRepository userRepository, IPasswordService passwordService, IJWTService jwtService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;

        }

        public class UserDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(6)]
            [MaxLength(20)]
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO userDTO)
        {
            var user = await _userRepository.GetAsync(u => u.Email == userDTO.Email);

            if(user == null)
                return Unauthorized();

            if(!_passwordService.Verify(userDTO.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            var token = _jwtService.CreateToken(user.Id, []); // TODO: fix roles when role tech is done

            return Ok(token);
        }

        public class RegisterUserDTO : UserDTO
        {
            [Required]
            [MinLength(3)]
            [MaxLength(20)]
            public string Username { get; set; }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO userDTO)
        {
            var user = await _userRepository.GetAsync(u => 
                u.Username == userDTO.Username || u.Email == userDTO.Email);

            if (user != null)
                return BadRequest("Username or email already exists");

            PasswordDTO passwordDTO = _passwordService.Hash(userDTO.Password);

            user = await _userRepository.CreateAsync(new()
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = passwordDTO.Hash,
                PasswordSalt = passwordDTO.Salt,
                CretedAt = DateTime.UtcNow
            });       

            // TODO: fix roles when role tech is done
            return Ok(_jwtService.CreateToken(user.Id, []));
        }
    }
}
