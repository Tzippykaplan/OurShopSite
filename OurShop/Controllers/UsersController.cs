using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Tzippy", "Chani" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            Task<User> checkUser = _userService.getUserById(id);
            if (checkUser != null)
                return Ok(checkUser);
            else
                return NotFound();
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post  ([FromBody] User user)
        {
            try { 
            _userService.AddUser(user);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] User userToUpdate )
        {
            try
            {
                _userService.uppdateUser(id, userToUpdate);
               
            }
            catch (Exception e)
            {
                return BadRequest();
            } 
            return Ok();

        }
        [HttpPost("login")]
        public ActionResult Login([FromQuery]string email, [FromQuery] string password)
            
        {
           User checkUser = _userService.loginUser(email, password);
           if (checkUser!=null)
                        return Ok(checkUser);
                   else
            return NotFound();


        }
        [HttpPost("passwordStrength")]
        public ActionResult< int> checkPasswordStrength( [FromQuery] string password)

        {
            int passwordStrength = _userService.checkPasswordStrength( password);
            return passwordStrength;


        }

       

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
