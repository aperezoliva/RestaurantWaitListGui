using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWaitListGui
{
    public class CustomerStorage : Customers // the whole point of this class and using hashs sets is to show more of a data structures skills
    {                                          // I was torn between doing this or making a database but I figured doing this would be nice
        public HashSet<int> custIdStorage;

        public CustomerStorage()
        {
            custIdStorage = new HashSet<int>();
        }

        public void storeCustId(Customers cust)
        {
            custIdStorage.Add(cust.CustId);
        }

        public bool checkSignIn(int id)
        {
            return custIdStorage.Contains(id);
        }

        public bool checkAdd(Customers cust)
        {
            return custIdStorage.Add(cust.CustId); // Testing purposes
        }

        public List<int> hashToList(List<int> list)
        {
           return list = custIdStorage.ToList();
        }
    }
}
