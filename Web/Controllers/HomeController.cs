using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BUS;
using laptop_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace laptop_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly LaptopBUS laptopBUS = new LaptopBUS();
        private readonly UserBUS userBUS = new UserBUS();
        private bool CheckSession()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserName")))
            {
                ViewData["CurrentUserName"] = HttpContext.Session.GetString("CurrentUserName");
                result = true;
            }
            return result;
        }
        public IActionResult Index()
        {
            DataTable laptopPreviewInfo = laptopBUS.GetLaptopPreviewInfo();
            CheckSession();
            return View(laptopPreviewInfo);
        }
        [HttpPost]
        public IActionResult Detail()
        {
            Laptop requiredLaptop;
            try
            {
                CheckSession();
                int laptopID = int.Parse(HttpContext.Request.Form["laptopID"]);
                DataTable laptopDetailTable = laptopBUS.GetLaptopDetail(laptopID);
                requiredLaptop = new Laptop()
                {
                    LaptopID = laptopID,
                    LaptopName = HttpContext.Request.Form["laptopName"],
                    LaptopCPU = (string)laptopDetailTable.Rows[0]["LaptopCPU"],
                    LaptopGPU = (string)laptopDetailTable.Rows[0]["LaptopGPU"],
                    LaptopRAM = (string)laptopDetailTable.Rows[0]["LaptopRAM"],
                    LaptopStorage = (string)laptopDetailTable.Rows[0]["LaptopStorage"],
                    LaptopDisplay = (string)laptopDetailTable.Rows[0]["LaptopDisplay"],
                    LaptopPrice = double.Parse(HttpContext.Request.Form["laptopPrice"]),
                    LaptopImage = HttpContext.Request.Form["laptopImage"],
                    LaptopDiscountPercentage = float.Parse(HttpContext.Request.Form["laptopDiscountPercentage"])
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(requiredLaptop);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DoSignUp()
        {
            string email = HttpContext.Request.Form["Email"];
            string password = HttpContext.Request.Form["Password"];
            string rePassword = HttpContext.Request.Form["RePassword"];
            if (!password.Equals(rePassword))
            {
                string notification = "Your Repeat Password isn't match your Password";
                return View("SignUp", notification);
            }
            else
            {
                bool result;
                try
                {
                    result = userBUS.SignUp(email, password);
                }
                catch (SqlException)
                {
                    string notification = "This Email has been used, please choose another Email";
                    return View("SignUp", notification);
                }
                
                if (result)
                {
                    string notification = "Sign Up Successful";
                    return View("SignUp", notification);
                } else
                {
                    string notification = "Sign Up Fail";
                    return View("SignUp", notification);
                }
            }
        }
        public IActionResult SignIn()
        {
            bool result = CheckSession();
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            ViewData.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CheckSignIn()
        {
            try
            {
                string email = HttpContext.Request.Form["Email"];
                string password = HttpContext.Request.Form["Password"];
                DataTable userDetail = userBUS.SignIn(email, password);
                #region CheckNullAddressAndPhone
                if (userDetail.Rows[0]["UserAddress"].GetType().ToString().Contains("DBNull"))
                {
                    HttpContext.Session.SetString("CurrentUserAddress", "No Information");
                } else
                {
                    HttpContext.Session.SetString("CurrentUserAddress", (string)userDetail.Rows[0]["UserAddress"]);
                }
                if (userDetail.Rows[0]["UserPhone"].GetType().ToString().Contains("DBNull"))
                {
                    HttpContext.Session.SetString("CurrentUserPhone", "No Information");
                } else
                {
                    HttpContext.Session.SetString("CurrentUserPhone", (string)userDetail.Rows[0]["UserPhone"]);
                }
                #endregion CheckNullAddressAndPhone
                HttpContext.Session.SetString("CurrentUserEmail", email);
                HttpContext.Session.SetString("CurrentUserName", (string)userDetail.Rows[0]["UserName"]);
                HttpContext.Session.SetString("CurrentUserRole", (string)userDetail.Rows[0]["UserRole"]);
                
            }
            catch (IndexOutOfRangeException) //Check Sai Email & Password
            {
                string error = "Wrong email or password";
                return View("SignIn", error);
            } catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
        public IActionResult Profile()
        {
            User user = new User()
            {
                UserEmail = HttpContext.Session.GetString("CurrentUserEmail"),
                UserName = HttpContext.Session.GetString("CurrentUserName"),
                UserRole = HttpContext.Session.GetString("CurrentUserRole"),
                UserAddress = HttpContext.Session.GetString("CurrentUserAddress"),
                UserPhone = HttpContext.Session.GetString("CurrentUserPhone")
            };
            CheckSession();
            return View(user);
        }
        public IActionResult Cart()
        {
            CheckSession();
            return View();
        }
        [HttpPost] 
        public IActionResult AddToCart()
        {
            int laptopID = int.Parse(HttpContext.Request.Form["LaptopID"]);
            return RedirectToAction("Index");
        }
    }
}
