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
        private readonly OrderBUS orderBUS = new OrderBUS();
        private readonly JsonUtility jsonUtility = new JsonUtility();
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
                int laptopID = int.Parse(HttpContext.Request.Form["laptopID"]);
                bool checkSignIn = CheckSession();
                if (!checkSignIn)
                {
                    ViewData["checkSignIn"] = "Haven't sign in";
                } else
                {
                    // Check session
                    if (HttpContext.Session.Keys.Contains("cart"))
                    {
                        // Get list from session
                        string jsonString = HttpContext.Session.GetString("cart");
                        List<Laptop> listLaptopInCart = jsonUtility.GetObjectFromJson<Laptop>(jsonString);
                        if (listLaptopInCart.Count >= 7)
                        {
                            ViewData["Notification"] = "Can not add more item. Cart can only contains " +
                                "less than or equal 7 items into cart";
                        } else
                        {

                        }
                        for (int i = 0; i < listLaptopInCart.Count; i++)
                        {
                            if (laptopID == listLaptopInCart[i].LaptopID)
                            {
                                if (listLaptopInCart[i].LaptopQuantity <= listLaptopInCart[i].LaptopOrderQuantity)
                                {
                                    ViewData["Notification"] = "This product only have " +
                                        listLaptopInCart[i].LaptopOrderQuantity + " left." +
                                        " Can't add more to cart.";
                                }
                            }
                        }
                    }
                }
                int laptopQuantity = laptopBUS.GetLaptopQuantity(laptopID);
                // Check Quantity
                if (laptopQuantity == 0)
                {
                    ViewData["Notification"] = "This product is out of stock";
                }
                //
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
                    LaptopQuantity = laptopQuantity,
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
            List<Laptop> listLaptopInCart = null;
            if (HttpContext.Session.Keys.Contains("cart"))
            {
                string jsonString = HttpContext.Session.GetString("cart");
                listLaptopInCart = jsonUtility.GetObjectFromJson<Laptop>(jsonString);
            } else
            {
                ViewData["cart"] = "You haven't add any product to cart";
            }
            return View(listLaptopInCart);
        }
        [HttpPost] 
        public IActionResult AddToCart()
        {
            int laptopID = int.Parse(HttpContext.Request.Form["LaptopID"]);
            string laptopName = HttpContext.Request.Form["LaptopName"];
            double laptopPrice = double.Parse(HttpContext.Request.Form["LaptopPrice"]);
            float laptopDiscountPercentage = float.Parse(HttpContext.Request.Form["LaptopDiscountPercentage"]);
            int laptopQuantity = int.Parse(HttpContext.Request.Form["LaptopQuantity"]);
            bool checkExist = false;
            try
            {
                // Check if cart is exist
                if (HttpContext.Session.Keys.Contains("cart"))
                {
                    // Get list from session
                    string jsonString = HttpContext.Session.GetString("cart");
                    List<Laptop> listLaptopInCart = jsonUtility.GetObjectFromJson<Laptop>(jsonString);
                    // If item exist, plus one in quantity
                    for (int i = 0; i < listLaptopInCart.Count; i++)
                    {
                        if (laptopID == listLaptopInCart[i].LaptopID)
                        {                            
                            listLaptopInCart[i].LaptopOrderQuantity++;
                            checkExist = true;
                        }
                    }
                    // If item doesn't exist, add to list
                    if (checkExist == false)
                    {
                        if (listLaptopInCart.Count >= 7)
                        {
                            ViewData["Quantity"] = "Can not add more than 7 items to cart";
                            return RedirectToAction("Detail");
                        } else
                        {
                            listLaptopInCart.Add(new Laptop()
                            {
                                LaptopID = laptopID,
                                LaptopName = laptopName,
                                LaptopPrice = laptopPrice,
                                LaptopQuantity = laptopQuantity,
                                LaptopDiscountPercentage = laptopDiscountPercentage,
                                LaptopOrderQuantity = 1
                            });
                        }
                    }
                    jsonString = jsonUtility.SetObjectAsJson<Laptop>(listLaptopInCart);
                    HttpContext.Session.SetString("cart", jsonString);
                }
                // If cart isn't exist
                else
                {
                    List<Laptop> listLaptopInCart = new List<Laptop>();
                    listLaptopInCart.Add(new Laptop()
                    {
                        LaptopID = laptopID,
                        LaptopName = laptopName,
                        LaptopPrice = laptopPrice,
                        LaptopQuantity = laptopQuantity,
                        LaptopDiscountPercentage = laptopDiscountPercentage,
                        LaptopOrderQuantity = 1
                    });
                    string jsonString = jsonUtility.SetObjectAsJson<Laptop>(listLaptopInCart);
                    HttpContext.Session.SetString("cart", jsonString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Order()
        {
            bool result = false;
            double totalPrice = double.Parse(HttpContext.Request.Form["totalPrice"]);
            // Check if cart is exist
            if (HttpContext.Session.Keys.Contains("cart"))
            {
                // Get list from session
                string jsonString = HttpContext.Session.GetString("cart");
                List<Laptop> listLaptopInCart = jsonUtility.GetObjectFromJson<Laptop>(jsonString);
                // Create Order
                int currentOrderID = orderBUS.GetOrderID();
                DataTable newOrder = new DataTable();
                newOrder.Rows[0]["OrderID"] = currentOrderID + 1;
                newOrder.Rows[0]["UserEmail"] = HttpContext.Session.GetString("CurrentUserEmail");
                newOrder.Rows[0]["OrderPrice"] = totalPrice;
                newOrder.Rows[0]["OrderDate"] = DateTime.Now;
                //Order newOrder = new Order()
                //{
                //    OrderID = currentOrderID + 1,
                //    UserEmail = HttpContext.Session.GetString("CurrentUserEmail"),
                //    OrderPrice = totalPrice,
                //    OrderDate = DateTime.Now
                //};
                // Save Order to DB
                result = orderBUS.AddNewOrder(newOrder);
                // Create listOrderUnit
                DataTable listOrderUnit = new DataTable();
                int currentOrderUnitID = orderBUS.GetOrderUnitID();
                for (int i = 0; i < listLaptopInCart.Count; i++)
                {
                    listOrderUnit.Rows[i]["OrderUnitID"] = currentOrderID + 1;
                    listOrderUnit.Rows[i]["OrderID"] = (int)newOrder.Rows[0]["OrderID"];
                    listOrderUnit.Rows[i]["LaptopID"] = listLaptopInCart[i].LaptopID;
                    listOrderUnit.Rows[i]["Quantity"] = listLaptopInCart[i].LaptopOrderQuantity;
                    listOrderUnit.Rows[i]["Price"] = listLaptopInCart[i].LaptopPrice * (100 - listLaptopInCart[i].LaptopDiscountPercentage);
                    currentOrderUnitID++;
                }
                //OrderUnit orderUnit = new OrderUnit()
                //{
                //    OrderUnitID = currentOrderUnitID + 1,
                //    OrderID = (int)newOrder.Rows[0]["OrderID"],
                //    LaptopID = laptop.LaptopID,
                //    Quantity = laptop.LaptopOrderQuantity,
                //    Price = (laptop.LaptopPrice * (100 - laptop.LaptopDiscountPercentage))
                //};
                // Save listOrderUnit to DB
                result = orderBUS.AddNewOrderUnit(listOrderUnit);
                HttpContext.Session.SetString("OrderAddingResult", result.ToString());
            }
            return RedirectToAction("OrderHistory");
        }
        public IActionResult OrderHistory()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("OrderAddingResult") {
                return View();
            } else
            {
                string result = HttpContext.Session.GetString("OrderAddingResult");
                ViewData["OrderAddingResult"] = result;
                return View();
            }
        }
    }
}
