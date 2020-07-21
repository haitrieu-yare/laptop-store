using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BUS
{
    public class OrderBUS
    {
        readonly OrderDAL orderDAL = new OrderDAL();
        public int GetOrderID()
        {
            return orderDAL.GetOrderID();
        }
        public int GetOrderUnitID()
        {
            return orderDAL.GetOrderUnitID();
        }
        public bool AddNewOrder(DataTable newOrder)
        {
            return orderDAL.AddNewOrder(newOrder);
        }
        public bool AddNewOrderUnit(DataTable newOrderUnit)
        {
            return orderDAL.AddNewOrderUnit(newOrderUnit);
        }
        public DataTable GetOrder(int begin, int end, string userEmail)
        {
            return orderDAL.GetOrder(begin, end, userEmail);
        }
        public DataTable GetOrderUnit(int orderID)
        {
            return orderDAL.GetOrderUnit(orderID);
        }
    }
}
