using AlteraBarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
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
        

        //private int count;
        [System.Web.Http.Route("api/admin/Register")]
        public IHttpActionResult Register(Admin user)
        {
            bool usernameAlreadyExists = entities.Admins.Any(person => person.UserName == user.UserName);
            if (ModelState.IsValid && !usernameAlreadyExists)
            {
                using (entities)
                {
                    entities.uspAddAdmin(user.UserName, user.PassWord, user.FirstName, user.LastName, user.ContactNumer);
                    //entities.uspAddAdmin().Add(user);
                    entities.SaveChanges();
                    return Ok();
                    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                    //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.UserName }));
                    //return (IHttpActionResult)response;
                }

            }
            return BadRequest(/*HttpStatusCode.InternalServerError*/);

        }
        [Route("api/admin/Login")]
        [HttpPost]
        public HttpResponseMessage Login(Admin userLogin)
        {
            if (ModelState.IsValid)
            {
                using (entities)
                {
                    var _currentUser = entities.Admins.FirstOrDefault(user => user.UserName == userLogin.UserName && user.PassWord == userLogin.PassWord);
                    if (_currentUser != null)
                    {
                        AdminId = _currentUser.ID;
                        Debug.WriteLine("Success &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                        return Request.CreateResponse(HttpStatusCode.Created,
                                             new
                                             {
                                                 Success = true,
                                                 RedirectUrl = ("https://localhost:44369/Administrator/UserList"),
                                                 Id = AdminId,


                                             });

                    }
                }
            }
            return null;
        }
        [System.Web.Http.Route("api/Admin/Style")]
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
        }
        [System.Web.Http.Route("api/admin/facial")]
        [System.Web.Http.HttpGet]
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
        [System.Web.Http.Route("api/admin/Appointment")]
        [System.Web.Http.HttpGet]
        public IEnumerable<AppointmentsInformation_Result> Appointment()
        {
            return entities.AppointmentsInformation().ToList();
        }
        [System.Web.Http.Route("api/admin/GetUsers")]
        [System.Web.Http.HttpGet]
        public IEnumerable<UserDetailsList_Result> GetUsers()
        {
            return entities.UserDetailsList().ToList();
        }
        [Route("api/admin/UpdateItems")]
        [HttpPut]
        public IHttpActionResult UpdateItem(Style item)
        {
            ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
            var result = entities.UpdateHairStyles(item.ID, item.Style1, item.Price, responseMessage);
            return Json(result);
        }

        [System.Web.Http.Route("api/admin/StyleAdd")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage StyleAdd(Style model)
        {
            try
            {
                entities.AddHairStyles(model.Style1, model.Price);
                entities.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch (Exception exception)
            {
                
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                
                return response;
            }
        }
        [Route("api/admin/UpdateFacial")]
        [HttpPut]
        public IHttpActionResult UpdateFacial(Facial item)
        {
            ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
            var result = entities.UpdateFacials(item.ID, item.Facial1, item.Price, responseMessage);
            return Json(result);
        }

        [System.Web.Http.Route("api/admin/FacialAdd")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage FacialAdd(Facial model)
        {
            try
            {
                
                entities.AddFacials(model.Facial1, model.Price);
                entities.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch (Exception exception)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

                return response;
            }
        }

        public HttpResponseMessage DeleteFacial(int id)
        {
            Facial facial = entities.Facials.Find(id);
                if (facial != null)
            {
                entities.Facials.Remove(facial);
                entities.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            return null;
        }
        


    }
}
                    

