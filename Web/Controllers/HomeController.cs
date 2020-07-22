using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BUS;
using laptop_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
                // Check SignIn de hien thi UserName va ngan User Add To Cart khi chua SignIn
                bool checkSignIn = CheckSession();
                if (!checkSignIn)
                {
                    ViewData["checkSignIn"] = "Haven't sign in";
                }
                else 
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
                        }
                        for (int i = 0; i < listLaptopInCart.Count; i++)
                        {
                            if (laptopID == listLaptopInCart[i].LaptopID)
                            {
                                if (listLaptopInCart[i].LaptopQuantity <= listLaptopInCart[i].LaptopOrderQuantity)
                                {
                                    ViewData["Notification"] = "Out of stock";
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
                UserAddress = HttpContext.Session.GetString("CurrentUserAddress"),
                UserPhone = HttpContext.Session.GetString("CurrentUserPhone")
            };
            bool result = CheckSession();
            // Check SignIn
            if (!result)
            {
                return RedirectToAction("SignIn");
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("EnterInformartion")))
            {
                ViewData["EnterInformartion"] = HttpContext.Session.GetString("EnterInformartion");
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult ChangeProfile()
        {
            try
            {
                string userEmail = HttpContext.Request.Form["UserEmail"];
                string userName = HttpContext.Request.Form["UserName"];
                string userAddress = HttpContext.Request.Form["UserAddress"];
                string userPhone = HttpContext.Request.Form["UserPhone"];
                Regex regex = new Regex(@"^0[0-9]{9}$");
                if (userPhone.Length > 0 && !regex.IsMatch(userPhone))
                {
                    HttpContext.Session.SetString("EnterInformartion", "Phone must be number with 10 digits, starting with 0");
                    return RedirectToAction("Profile");
                }
                bool result = userBUS.UpdateProfile(userEmail, userName, userAddress, userPhone);
                if (result)
                {
                    HttpContext.Session.SetString("EnterInformartion", "Update Successful");
                    HttpContext.Session.SetString("CurrentUserName", userName);
                    HttpContext.Session.SetString("CurrentUserAddress", userAddress);
                    HttpContext.Session.SetString("CurrentUserPhone", userPhone);
                } else
                {
                    HttpContext.Session.SetString("EnterInformartion", "Update Fail");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Profile");
        }
        public IActionResult Cart()
        {
            List<Laptop> listLaptopInCart = null;
            bool result = CheckSession();
            // Check SignIn
            if (!result)
            {
                return RedirectToAction("SignIn");
            } else
            {
                if (HttpContext.Session.Keys.Contains("cart"))
                {
                    string jsonString = HttpContext.Session.GetString("cart");
                    listLaptopInCart = jsonUtility.GetObjectFromJson<Laptop>(jsonString);
                }
                else
                {
                    ViewData["cart"] = "You haven't add any product to cart";
                }
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
            // Check Phone, Adress
            if (HttpContext.Session.GetString("CurrentUserAddress").Equals("No Information") 
                || HttpContext.Session.GetString("CurrentUserPhone").Equals("No Information"))
            {
                HttpContext.Session.SetString("EnterInformartion", "Please enter your address and phone before order");
                return RedirectToAction("Profile");
            } else
            {
                bool result;
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
                    newOrder.Columns.Add("OrderID");
                    newOrder.Columns.Add("UserEmail");
                    newOrder.Columns.Add("OrderPrice");
                    DataRow drOrder = newOrder.NewRow();
                    drOrder["OrderID"] = currentOrderID + 1;
                    drOrder["UserEmail"] = HttpContext.Session.GetString("CurrentUserEmail");
                    drOrder["OrderPrice"] = totalPrice;
                    newOrder.Rows.Add(drOrder);
                    // Save Order to DB
                    result = orderBUS.AddNewOrder(newOrder);
                    if (result)
                    {
                        // Create listOrderUnit
                        DataTable listOrderUnit = new DataTable();
                        listOrderUnit.Columns.Add("OrderUnitID");
                        listOrderUnit.Columns.Add("OrderID");
                        listOrderUnit.Columns.Add("LaptopID");
                        listOrderUnit.Columns.Add("Quantity");
                        listOrderUnit.Columns.Add("Price");
                        int currentOrderUnitID = orderBUS.GetOrderUnitID();
                        for (int i = 0; i < listLaptopInCart.Count; i++)
                        {
                            DataRow drOrderUnit = listOrderUnit.NewRow();
                            drOrderUnit["OrderUnitID"] = currentOrderUnitID + 1;
                            drOrderUnit["OrderID"] = newOrder.Rows[0]["OrderID"];
                            drOrderUnit["LaptopID"] = listLaptopInCart[i].LaptopID;
                            drOrderUnit["Quantity"] = listLaptopInCart[i].LaptopOrderQuantity;
                            drOrderUnit["Price"] = listLaptopInCart[i].LaptopPrice * (100 - listLaptopInCart[i].LaptopDiscountPercentage) / 100;
                            listOrderUnit.Rows.Add(drOrderUnit);
                            currentOrderUnitID++;
                        }
                        // Save listOrderUnit to DB
                        result = orderBUS.AddNewOrderUnit(listOrderUnit);
                    }
                    if (result)
                    {
                        for (int i = 0; i < listLaptopInCart.Count; i++)
                        {
                            int laptopNewQuantity = listLaptopInCart[i].LaptopQuantity - listLaptopInCart[i].LaptopOrderQuantity;
                            result = laptopBUS.UpdateLaptopQuantity(listLaptopInCart[i].LaptopID, laptopNewQuantity);
                        }
                    }
                    HttpContext.Session.SetString("OrderAddingResult", result.ToString());
                    // Delete cart when finished
                    HttpContext.Session.Remove("cart");
                }
                return RedirectToAction("OrderHistory");
            }
        }
        public IActionResult OrderHistory()
        {
            DataTable listOrder;
            bool resultSession = CheckSession();
            // Check SignIn
            if (!resultSession)
            {
                return RedirectToAction("SignIn");
            } else
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("OrderAddingResult")))
                {
                    string result = HttpContext.Session.GetString("OrderAddingResult");
                    ViewData["OrderAddingResult"] = result;
                }
                string userEmail = HttpContext.Session.GetString("CurrentUserEmail");
                listOrder = orderBUS.GetOrder(1, 20, userEmail);
            }
            return View(listOrder);
        }
        [HttpPost]
        public IActionResult OrderDetail()
        {
            DataTable listOrderUnit;
            bool result = CheckSession();
            // Check SignIn
            if (!result)
            {
                return RedirectToAction("SignIn");
            } else
            {
                int orderID = int.Parse(HttpContext.Request.Form["OrderID"]);
                listOrderUnit = orderBUS.GetOrderUnit(orderID);
            }
            return View(listOrderUnit);
        }
    }
}
