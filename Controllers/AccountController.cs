using AlteraBarberShop.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using NLog;



namespace AlteraBarberShop.Controllers
{
    public class AccountController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BarbersShopEntities entities = new BarbersShopEntities();
        public static int UserId;
        //private int count;
        [System.Web.Http.Route("api/account/Register")]
        public IHttpActionResult Register(NewUser user)
        {
            bool usernameAlreadyExists = entities.Users.Any(person => person.UserName == user.UserName);
            if (ModelState.IsValid && !usernameAlreadyExists)
            {
                using (entities)
                {
                    entities.UserRegister(user.UserName, user.Password, user.FirstName, user.LastName, user.ContactNumber, user.Address);
                    entities.SaveChanges();
                    return Ok();
                    
                }
                
            }
            return BadRequest(/*HttpStatusCode.InternalServerError*/);
            //BarbersShopEntities entities = new BarbersShopEntities();


        }
        [System.Web.Http.Route("api/account/Login")]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Login")]
        public HttpResponseMessage Login(User userLogin)
        {

            if (ModelState.IsValid)
            {
                using (entities)
                {
                    try
                    {
                        
                        var _currentUser = entities.Users.FirstOrDefault(user => user.UserName == userLogin.UserName && user.PassWord == userLogin.PassWord);
                        if (_currentUser != null)
                        {
                            //HttpRequest.HttpContext.Session["UserId"] = _currentUser.FirstName.ToString();
                            string userName = userLogin.UserName;
                            UserId = _currentUser.ID;
                            Debug.WriteLine("Success &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                            logger.Info("User Logged in Successfully");
                            return Request.CreateResponse(HttpStatusCode.Created,
                                                 new
                                                 {

                                                     Success = true,
                                                     RedirectUrl = ("https://localhost:44369/Home/index"),
                                                     Id = UserId,
                                                     UserName = userName

                                                 });
                            
                        }
                        logger.Error("User data is Invalid");
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error Occoured During Adding User Data In Database");
                        logger.Error(ex);

                    }
                    
                }
                
            }
            logger.Error("ModelState is Invalid");
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
            
            return Ok(style);
            

        }
        [System.Web.Http.Route("api/Account/facial")]
        [System.Web.Http.HttpGet]
        //public IHttpActionResult Facials()
        public IEnumerable<AllFacials_Result> Facial()
        {
            return (IEnumerable<AllFacials_Result>)entities.AllFacials().ToList();
           
        }
       
        [System.Web.Http.Route("api/Account/Appointment")]
        [System.Web.Http.HttpPost]
        public void Appointment(AppointmentView user)
        {
            if (ModelState.IsValid)
            {
                using (entities)
                {
                    entities.InsertAppointment(user.UserID, user.StyleId, user.FacialId, user.StatusId, Convert.ToDateTime(user.DateTime), user.Address);
                    entities.SaveChanges();
                }

            }
            logger.Info("Appointment Has Booked Succesfully");
            Console.Write("User Has been Booked Appointment Suucessfully  ");

        }
        
        
    }
    

}