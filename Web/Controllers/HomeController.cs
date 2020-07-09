using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BUS;
using Microsoft.AspNetCore.Mvc;

namespace laptop_store.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            LaptopBUS laptopBUS = new LaptopBUS();
            List<string> ImageList = laptopBUS.GetLaptopImage();
            return View(ImageList);
        }
    }
}
