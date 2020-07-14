using System;
using System.Collections.Generic;
using System.Data;
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
        public IActionResult Index()
        {
            DataTable laptopPreviewInfo = laptopBUS.GetLaptopPreviewInfo();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IsLogin")))
            {
                ViewData["CurrentUser"] = HttpContext.Session.GetString("CurrentUserName");
            }
            return View(laptopPreviewInfo);
        }
        [HttpPost]
        public IActionResult Detail()
        {
            Laptop requiredLaptop;
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IsLogin")))
                {
                    ViewData["CurrentUser"] = HttpContext.Session.GetString("CurrentUserName");
                }
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
        public IActionResult SignIn()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IsLogin")))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CheckSignIn()
        {
            User user;
            try
            {
                string email = HttpContext.Request.Form["Email"];
                string password = HttpContext.Request.Form["Password"];
                DataTable userDetail = userBUS.SignIn(email, password);
                user = new User()
                {
                    UserEmail = email,
                    UserID = (int)userDetail.Rows[0]["UserID"],
                    UserName = (string)userDetail.Rows[0]["UserName"],
                    UserAddress = (string)userDetail.Rows[0]["UserAddress"],
                    UserPhone = (string)userDetail.Rows[0]["UserPhone"],
                    UserRole = (string)userDetail.Rows[0]["UserRole"]
                };
                HttpContext.Session.SetString("CurrentUserName", user.UserName);
                HttpContext.Session.SetString("IsLogin", "True");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}
