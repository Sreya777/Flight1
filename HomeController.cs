using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Controllers;



namespace Web_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserRegisterModel Ur)
        {
            //RegisterController reg = new RegisterController();



            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("http://localhost:60308/Register/custregform");
                var insertrec = hc.PostAsJsonAsync<UserRegisterModel>("custregform", Ur);//Asynchronosly passing the values in Json Format to API
                var savrec = insertrec.Result;//Saving the User Details



                //Condition for Successfull Registartion
                if ((int)savrec.StatusCode == 200)
                {
                    ViewBag.Successmessage = "Successfully Registered!!!!!";
                }
                //Condition for User Already Existing Check
                if ((int)savrec.StatusCode == 422)
                {
                    ViewBag.userAlreadymessage = "User Name Already Exist in System, please provide a new User Name";
                }
                //Condition for Mobile Num Should be 10 digits
                if ((int)savrec.StatusCode == 423)
                {
                    ViewBag.MobileLengthmessage = "Mobile Number Should be 10 Digits Only";
                }
                //Condition for Mobile Num Already Exist
                if ((int)savrec.StatusCode == 424)
                {
                    ViewBag.MobileExistmessage = "Mobile Number Already Exist in System, please provide a new Mobile Number";
                }
                //Condition for Email Id Already Exist
                if ((int)savrec.StatusCode == 425)
                {
                    ViewBag.EmailExistmessage = "Email Already Exist in System, please provide a new Email ID";
                }
            }
            return View();



        }



        public ActionResult Sample()
        {
            return null;
        }
    }
}