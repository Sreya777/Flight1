using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;
using Web_API.Controllers;

namespace Web_API.Controllers
{
    public class RegisterController : ApiController
    {

        RegisterDBEntities objRegisterDBEntities;
        public RegisterController()
        {
            objRegisterDBEntities = new RegisterDBEntities();
        }

        public IHttpActionResult custregform(UserRegisterModel Ur)
        {
            RegisterDBEntities nd = new RegisterDBEntities();



            if (objRegisterDBEntities.Customers.Any(model => model.Username == Ur.Username))
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse((HttpStatusCode)422, new HttpError("Something goes wrong")));
            }

            else if (Ur.MobileNumber.Length > 10)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse((HttpStatusCode)423, new HttpError("Something goes wrong")));
            }



            else if (objRegisterDBEntities.Customers.Any(model => model.MobileNumber == Ur.MobileNumber))
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse((HttpStatusCode)424, new HttpError("Something goes wrong")));
            }
            else if (objRegisterDBEntities.Customers.Any(model => model.EmailID == Ur.EmailID))
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse((HttpStatusCode)425, new HttpError("Something goes wrong")));
            }
            else
            {
                nd.Customers.Add(new Customer()
                {
                    CustomerID = Ur.CustomerID,
                    Firstname = Ur.Firstname,
                    Lastname = Ur.Lastname,
                    Age = Ur.Age,
                    Gender = Ur.Gender,
                    EmailID = Ur.EmailID,
                    MobileNumber = Ur.MobileNumber,
                    Username = Ur.Username,
                    Password = Ur.Password,
                });
                nd.SaveChanges();
                return Ok();
            }
        }
    }
}
