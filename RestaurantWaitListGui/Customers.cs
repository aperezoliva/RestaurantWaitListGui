using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantWaitListGui
{
    public class Customers
    {
        private int custId;
        private int custPriority;
        public string custName;
        public int CustId { get => custId; set => custId = value; }
        public int CustPriority { get => custPriority; set => custPriority = value; }
        public string CustName { get=> custName; set => custName = value; }

        public Customers()
        {

        }

        public Customers(int id, int priority) // This just creates a customer class which will be transferred over to a storage
        {
            this.CustPriority = priority;
            this.custId = id;

        }
    }
}
