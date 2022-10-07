using AlteraBarberShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace AlteraBarberShop.Controllers
{
    public class AccountController : ApiController
    {
        BarbersShopEntities entities = new BarbersShopEntities();
        private int UserId;
        //private int count;

        public IHttpActionResult Register(NewUser user)
        {
            //BarbersShopEntities entities = new BarbersShopEntities();
            entities.uspAddUser(user.UserName, user.Password, user.FirstName, user.LastName, user.ContactNumber, user.Address);
            entities.SaveChanges();
            return Ok();

        }
        [System.Web.Http.Route("api/account/Login")]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Login")]
        public HttpResponseMessage Login(User userLogin)
        {
            if (ModelState.IsValid)
            {
                using (entities)
                {
                    var _currentUser = entities.Users.FirstOrDefault(user => user.UserName == userLogin.UserName && user.PassWord == userLogin.PassWord);
                    //var _currentUser = entities.Users.Where(user => user.UserName == userLogin.UserName && user.PassWord == userLogin.PassWord).Select(UserId => new User
                    //{
                    string userName = userLogin.UserName;
                    
                    //PassWord = User.Password,
                    //}).SingleOrDefault();

                    //if (_currentUser == null)
                    //{
                    //    throw(Exception ex);
                    //}

                    {
                        UserId = _currentUser.ID;
                        //TODO: Redirect To Single app Page with this UserId
                        Debug.WriteLine("Success &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                        //Redirect("https://localhost:44304/User/Create");
                        //var newUrl = this.Url.Link("DefaultApi", new
                        //{
                        //    Controller = "Appointment",
                        //    Action = "Appointment"
                        //});
                        //var urlBuilder = new UrlHelper(Request.RequestContext);
                        return Request.CreateResponse(HttpStatusCode.Created,
                                             new
                                             {
                                                 Success = true,
                                                 RedirectUrl = ("https://localhost:44369/Home/Style"),
                                                 Id = UserId,
                                                 UserName = userName

                                             }) ;
                    }
                    return null;
                }
            }
            return null;
        }
        [System.Web.Http.Route("api/Account/Style")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Styles()
        {
            IEnumerable<Style> style = entities.HairStyles().Select(sty => new Style()
            {
                ID = sty.ID,
                Style1 = sty.Style,
                Price = Convert.ToInt32(sty.Price)

            }).ToList<Style>();
            IEnumerable<Style> styleList = style;
            return Ok(style);
            //    [System.Web.Http.Route("api/Account/Style")]
            //[System.Web.Http.HttpGet]
            //public IEnumerable<Style> Style()
            //{
            //    return entities.Styles.ToList();
            //}

        }
        [System.Web.Http.Route("api/Account/facial")]
        [System.Web.Http.HttpGet]
        //public IHttpActionResult Facials()
        public IEnumerable<AllFacials_Result> Facial()
        {
            //IEnumerable<Facial> facial = entities.AllFacials().Select(sty => new Facial()
            //{
            //    //ID = sty.ID,
            //    //Facial1 = sty.Facial,
            //    //Price = Convert.ToInt32(sty.Price)

            //}).ToList<Facial>();
            //IEnumerable<AllFacials_Result> facialRes = entities.AllFacials().ToList();
            //IEnumerable<Facial> facialList = facialRes.ToList();
            return (IEnumerable<AllFacials_Result>)entities.AllFacials().ToList();
            //return entities.Facials.ToList();
            //return Ok(facial);
        }
        [System.Web.Http.Route("api/Account/Appointment")]
        [System.Web.Http.HttpPost]

        public void Appointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                using (entities)
                {
                    //ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
                    entities.Appointments.Add(appointment);
                    entities.SaveChanges();
                }
            }
        }

    }
}
