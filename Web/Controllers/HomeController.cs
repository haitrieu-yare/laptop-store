using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BUS;
using laptop_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace laptop_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly LaptopBUS laptopBUS = new LaptopBUS();
        [HttpGet]
        public IActionResult Index()
        {
            DataTable laptopPreviewInfo = laptopBUS.GetLaptopPreviewInfo();
            return View(laptopPreviewInfo);
        }
        [HttpPost]
        public IActionResult Detail()
        {
            Laptop requiredLaptop;
            try
            {
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
    }
}
