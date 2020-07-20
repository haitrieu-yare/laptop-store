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
    }
}
