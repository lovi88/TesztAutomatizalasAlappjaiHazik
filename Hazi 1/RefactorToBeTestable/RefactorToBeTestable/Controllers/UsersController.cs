using RefactorToBeTestable.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class UsersController : ApiController
    {
        private IUserRepository repository;
        

        public UsersController()
        {
            repository = new UserRepository();
        }

        public UsersController(IUserRepository UserRepository)
        {
            repository = UserRepository;
        }


        // GET api/User/Name/PassHash
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserByNameAndPass(string Name, string PasswordHash)
        {
            User user = repository.GetUserByNameAndPassword(Name, PasswordHash);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
            //return user;
        }
        

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UId)
            {
                return BadRequest();
            }


            try
            {
                repository.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (ArgumentException) {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                repository.Create(user);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            
            return CreatedAtRoute("DefaultApi", new { id = user.UId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                repository.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}