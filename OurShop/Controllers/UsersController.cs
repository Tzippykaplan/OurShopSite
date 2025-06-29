using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using DTO;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace OurShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        IMapper _mapper;
        ILogger<User> _logger;
        public UsersController(IUserService userService, IMapper mapper, ILogger<User> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User checkUser = await _userService.getUserById(id);
            if (checkUser != null)
                return Ok(_mapper.Map<User, UserByIdDto>(checkUser));
            else
                return NotFound();
        }

        // POST api/<UsersController>
        [AllowAnonymous] 
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] AddUserDto user)
        {
            try
            {
                User ParsedUser = _mapper.Map<AddUserDto, User>(user);
               User newUser= await _userService.AddUser(ParsedUser);
               if(newUser==null)
               {
                    return Conflict();
               }
                return CreatedAtAction(nameof(Get), new { id = ParsedUser.UserId }, _mapper.Map<AddUserDto, ReturnPostUserDto>(user));
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] AddUserDto userToUpdate)
        {
            try
            {
              User uppdatedUser=  await _userService.updateUser(id, _mapper.Map<AddUserDto, User>(userToUpdate));
             return Ok(uppdatedUser);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
           


        }
        [AllowAnonymous] 
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                User checkUser = await _userService.loginUser(email, password);
                if (checkUser != null)
                {
                    _logger.LogInformation("user { checkUser.UserId } loged in", checkUser.UserId);

                    var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                    var jwtKey = config["Jwt:Key"];
                    var jwtIssuer = config["Jwt:Issuer"];
                    if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer))
                        return StatusCode(500, "JWT key or issuer is missing in configuration");

                    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var claims = new[]
                    {
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, checkUser.UserId.ToString()),
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, checkUser.Email)
                    };

                    var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                        issuer: jwtIssuer,
                        audience: null,
                        claims: claims,
                        expires: DateTime.Now.AddHours(2),
                        signingCredentials: credentials
                    );
                    var tokenString = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
                    Response.Cookies.Append(
                        "jwtToken",
                        tokenString,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.Now.AddHours(2)
                        });
                    return Ok(_mapper.Map<User, ReturnLoginUserDto>(checkUser));
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
        [HttpPost("passwordStrength")]
        public ActionResult<int> checkPasswordStrength([FromQuery] string password)

        {
            int passwordStrength = _userService.checkPasswordStrength(password);
            return passwordStrength;


        }



    }
}
