﻿using AlteraBarberShop.Models;
using Microsoft.Extensions.Logging;
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
                    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                    //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.UserName }));
                    //return (IHttpActionResult)response;
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
                            string userName = userLogin.UserName;
                            UserId = _currentUser.ID;
                            Debug.WriteLine("Success &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                            return Request.CreateResponse(HttpStatusCode.Created,
                                                 new
                                                 {
                                                     Success = true,
                                                     RedirectUrl = ("https://localhost:44369/Home/index"),
                                                     Id = UserId,
                                                     UserName = userName

                                                 });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);

                    }


                      
                }
                return null;
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
            //IEnumerable<Style> styleList = style;
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
        //[System.Web.Http.Route("api/Account/Appointment")]
        //[System.Web.Http.HttpPost]

        //public void Appointment(Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (entities)
        //        {
        //            //ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
        //            entities.Appointments.Add(appointment);
        //            entities.SaveChanges();
        //        }
        //    }
        //}
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
            Console.Write("User Has been Booked Appointment Suucessfully  ");

        }
        //[System.Web.Http.Route("api/Account/put")]
        //public HttpResponseMessage put(int UserID, Appointment model)
        //{
        //    try
        //    {
        //        if (UserID == model.UserID)
        //        {
        //            entities.Entry(model).State = System.Data.Entity.EntityState.Modified;
        //            entities.SaveChanges();
        //            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //            return response;
        //        }
        //        else
        //        {
        //            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
        //            return response;
        //        }

        //    }
        //    catch (Exception exception)
        //    {
        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

        //        return response;

        //    }
        //}
        [System.Web.Http.Route("api/Account/GetView")]
        public Appointment GetView(int UserId)
        {
            return entities.Appointments.Find(UserId);
        }
    }
    

}