using Common.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApplication4.Controllers
{
    public class UsersController : ApiController
    {
        [HttpPut]
        [Route("api/UsersController/Login")]
        public IHttpActionResult Login(string username, string password)
        {
            var user = Users.Login(username, password);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                var users = (Dictionary<User, DateTime>)HttpContext.Current.Application["users"] ?? new Dictionary<User, DateTime>();
                if (!users.ContainsKey(user))
                {
                    users.Add(user, DateTime.UtcNow);
                    HttpContext.Current.Application["users"] = users;
                }

                return Ok(user);
            }
        }

        [HttpPut]
        [Route("api/UsersController/Logout")]
        public IHttpActionResult Logout(int id)
        {
            var user = Users.GetById(id);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                var users = (Dictionary<User, DateTime>)HttpContext.Current.Application["users"] ?? new Dictionary<User, DateTime>();
                if (users.ContainsKey(user))
                {
                    return BadRequest();
                }
                users.Remove(user);
                HttpContext.Current.Application["users"] = users;
                return Ok();
            }
        }

        [HttpPost]
        [Route("api/UsersController/RegisterUser")]
        public IHttpActionResult RegisterUser([FromBody] User user)
        {
            var createdUser = Users.RegisterUser(user);
            if (createdUser == null)
            {
                return BadRequest();
            }

            return Ok(createdUser.Id);
        }

        [HttpPut]
        [Route("api/UsersController/RedoLogin")]
        public IHttpActionResult RedoLogin(int id)
        {
            var user = Users.GetById(id);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                var users = (Dictionary<User, DateTime>)HttpContext.Current.Application["users"] ?? new Dictionary<User, DateTime>();
                if (!users.ContainsKey(user))
                {
                    users.Add(user, DateTime.UtcNow);
                    HttpContext.Current.Application["users"] = users;
                }

                return Ok(user);
            }
        }

        //update korisnika
        [HttpPut]
        [Route("api/UsersController/UpdateAsync")]
        public async Task<IHttpActionResult> UpdateAsync(int id, [FromBody] User user)
        {
            if (!await Users.Update(id, user))
            {
                return BadRequest();
            }
            return Ok();
        }
        //odobravanje komentara
        [HttpGet]
        [Route("api/UsersController/GetById")]
        public IHttpActionResult GetById(int id) => Ok(Users.GetById(id));
        //Registrovani korisnici za grupni trening
        [HttpGet]
        [Route("api/UsersController/GetRegisteredVisitorsByGroupWork")]
        public IHttpActionResult GetRegisteredVisitorsByGroupWork(int id) => Ok(Users.GetRegisteredVisitorsByGroupWork(id));

        //Treneri vlasnika
        [HttpGet]
        [Route("api/UsersController/GetTrainersByOwner")]
        public IHttpActionResult GetTrainersByOwner(int ownerId) => Ok(Users.GetTrainersByOwner(ownerId));
        //Prijava za grupni trening
        [HttpPut]
        [Route("api/UsersController/AssigneAsync")]
        public async Task<IHttpActionResult> AssigneAsync(int id, int groupWorkId)
        {

            if (!await Users.AssigneUser(id, groupWorkId))
            {
                return BadRequest();
            }

            return Ok();
        }
        //Nista
        [HttpGet]
        [Route("api/UsersController/GetHistoryOfWorkOuts")]
        public IHttpActionResult GetHistoryOfWorkOuts(int id)
        {
            return Ok(Users.GetHistoryOfWorkOuts(id));
        }
// Ostavljanje komentara
        [HttpGet]
        [Route("api/UsersController/GetUserWorkOutsByFitness")]
        public IHttpActionResult GetUserWorkOutsByFitness(int userId, int fitnessId)
        {
            return Ok(Users.GetUserHistoryOfWorkOutsByFitness(userId, fitnessId));
        }

        //Blokiraj trenera
        [HttpPut]
        [Route("api/UsersController/BlockuUser")]
        public IHttpActionResult BlockUser(int ownerId, int userId)
        {
            return Ok(Users.BlockUserAsync(userId));
        }
    }
}