using Microsoft.AspNetCore.Mvc;
using pawbossAPI.Entities;
using pawbossAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pawbossAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.mapper = mapper;
        }

        // http://localhost:53024/api/users
        [HttpGet()]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var allUsers = _userRepository.GetUsers();
            return Ok(allUsers);
        }

        // http://localhost:53024/api/users/getUserById
        [Route("getUserById"), HttpPost]
        public IActionResult GetPet([FromBody] inputId userId)
        {
            var user = _userRepository.GetUser(userId.Id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // http://localhost:53024/api/users
        [HttpPost]
        public IActionResult Create([FromBody] UserAdd userAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string password = userAdd.Password;
            byte[] passwordHash, passwordSalt;
            Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var addingUser = mapper.Map<User>(userAdd);

            addingUser.PasswordHash = passwordHash;
            addingUser.PasswordSalt = passwordSalt;
            var result = _userRepository.CreateUser(addingUser);

            if (!result)
            {
                return BadRequest("Username is already used"); 
            }

            else
            {
                return Ok(addingUser);
            }
        }

        // http://localhost:53024/api/users
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserUpdate user)
        {
            var result = _userRepository.UpdateUser(user);
            if (result)
            {
                return Ok();
            }
            else
            {
                 return BadRequest("Username is already used");
            }
        }

        // http://localhost:53024/api/users/getUserByUsername
        [Route("getUserByUsername"), HttpPost]
        public IActionResult GetUserDetails([FromBody] UsernameDetails user)
        {
            var identifyUser = _userRepository.GetUserByUserName(user.Username);
            return Ok(identifyUser);
        }

        // http://localhost:53024/api/users/updateLoggedIn
        [Route("updateLoggedIn"), HttpPut]
        public IActionResult UpdateStatusLoggedIn([FromBody] UsernameDetails user)
        {
            _userRepository.UpdateStatusLoggedIn(user.Username);
            return Ok();
        }

        // http://localhost:53024/api/users/updateLoggedOut
        [Route("updateLoggedOut"), HttpPut]
        public IActionResult UpdateStatusLoggedOut([FromBody] UsernameDetails user)
        {
            _userRepository.UpdateStatusLoggedOut(user.Username);
            return Ok();
        }

        // http://localhost:53024/api/users
        [HttpDelete]
        public IActionResult DeletePermanetly([FromBody] User user)
        {
            var result = _userRepository.DeleteUser(user.Id);
            return Ok();
        }

        // http://localhost:53024/api/users/login
        [Route("login"), HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUserByUserName(userLogin.Username);

            //If such a username doesn't exist
            if (user == null)
            {
                return null;
            }

            //If username and password matches
            if (Utility.VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Ok(user);
            }
            else
            {
                return null;
            }
        }
    }
}
