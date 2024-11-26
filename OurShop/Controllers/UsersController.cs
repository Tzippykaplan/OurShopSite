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

        public UsersController(IUserService iUserService)
        {
            _userService = iUserService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Tzippy", "Kaplan" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User checkUser = _userService.getUserById(id);
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
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

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

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put (int id, [FromBody] User userToUpdate)
        {
            User checkUser = _userService.uppdateUser(id, userToUpdate);
            if (checkUser != null)
                return Ok(checkUser);
            else
                return BadRequest();
           

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
