using AlteraBarberShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace AlteraBarberShop.Controllers
{
    public class AdminController : ApiController
    {
        private BarbersShopEntities entities = new BarbersShopEntities();
        public static int AdminId;
        private string varhashedBytes;

        //private int count;
        [System.Web.Http.Route("api/admin/Register")]
        public IHttpActionResult Register(Admin user)
        {
            bool usernameAlreadyExists = entities.Admins.Any(person => person.UserName == user.UserName);
            if (ModelState.IsValid && !usernameAlreadyExists)
            {
                using (entities)
                {
                    entities.Admins.Add(user);
                    entities.SaveChanges();
                    return Ok();
                    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                    //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.UserName }));
                    //return (IHttpActionResult)response;
                }

            }
            return BadRequest(/*HttpStatusCode.InternalServerError*/);

        }
        public HttpResponseMessage Login(Admin userLogin)
        {
            if (ModelState.IsValid)
            {
                using (entities)
                {
                    var _currentUser = entities.Users.FirstOrDefault(user => user.UserName == userLogin.UserName && user.PassWord == varhashedBytes);
                    if (_currentUser != null)
                    {
                        AdminId = _currentUser.ID;
                        //TODO: Redirect To Single app Page with this UserId 
                        var newUrl = this.Url.Link("Default", new
                        {
                            Controller = "Account",
                            Action = "Index"
                        });
                        return Request.CreateResponse(HttpStatusCode.Created,
                                                 new { Success = true, RedirectUrl = newUrl });
                    }
                }
            }
            return null;
        }
    }

}
                    

