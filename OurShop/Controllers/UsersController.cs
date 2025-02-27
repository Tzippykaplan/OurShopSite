using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using DTO;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
        public async Task<IActionResult> Put(int id, [FromBody] AddUserDto userToUpdate)
        {
            try
            {
                await _userService.updateUser(id, _mapper.Map<AddUserDto, User>(userToUpdate));

            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();

        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromQuery] string email, [FromQuery] string password)

        {
            User checkUser = await _userService.loginUser(email, password);
            //if (checkUser != null)
                return Ok(_mapper.Map<User, ReturnLoginUserDto>(checkUser));
            //else
            //    return NotFound();


        }
        [HttpPost("passwordStrength")]
        public ActionResult<int> checkPasswordStrength([FromQuery] string password)

        {
            int passwordStrength = _userService.checkPasswordStrength(password);
            return passwordStrength;


        }



    }
}
