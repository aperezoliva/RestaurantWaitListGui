using RestaurantWaitListGui;
using System;
using System.Collections.Generic;
using Xunit;

namespace RestaurantWaitListTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestHashSet()
        {
            // ARRANGE
            Customers customerOne = new Customers(5555, 2);
            CustomerStorage customerStorage = new CustomerStorage();

            //ACT
            customerStorage.storeCustId(customerOne);

            //ASSERT
            Assert.True(customerStorage.checkSignIn(customerOne.CustId));
        }

        [Fact]

        public void TestHashSetSameCustomer()
        {
            // ARRANGE
            Customers customerOne = new Customers(5555, 2);
            Customers customerTwo = new Customers(5555, 2);
            CustomerStorage customerStorage = new CustomerStorage();

            //ACT
            customerStorage.storeCustId(customerOne);
            //ASSERT
            Assert.False(customerStorage.checkAdd(customerTwo)); // Hashsets throw false whenever the element that is trying to be added is already present
        }

        [Fact]

        public void TransferQueueToRest()
        {
            // ARRANGE
            Customers customerOne = new Customers(5555, 2);
            Customers customerTwo = new Customers(5556, 1);
            Restaurant restaurant = new Restaurant(); 
            Waitlist waitList = new Waitlist();

            // ACT
            waitList.assignWaitList(customerOne);
            waitList.assignWaitList(customerTwo);
            restaurant.getWaitList(waitList);
            restaurant.customerEnter(); // since customerTwo has a priority 1, they're first in the priority
            // ASSERT

            Assert.Contains(5556, restaurant.restaurantList); // should be 5556
        }

        [Fact]
        public void QueueDequesWhenTransfers()
        {
            Customers customerOne = new Customers(5555, 2);
            Customers customerTwo = new Customers(5556, 1);
            Restaurant restaurant = new Restaurant();
            Waitlist waitList = new Waitlist();

            // ACT
            waitList.assignWaitList(customerOne);
            waitList.assignWaitList(customerTwo);
            restaurant.getWaitList(waitList);
            restaurant.customerEnter(); // since customerTwo has a priority 1, they're first in the priority
            // ASSERT

            Assert.Equal(1, waitList.checkWaitListSize());

        }

        [Fact]
        public void TestCreateCustomerList()
        {
            // ARRANGE
            var customers = new List<Customers>();
            Random random = new Random();
            int randomCustAmt = 15;
            int ranCustId = random.Next(500, 505);
            int ranCustPriority = random.Next(1, 2);

            // ACT
            for (int i = 0; i < randomCustAmt; i++) 
            {
                customers.Add(new Customers { CustId = ranCustId, CustPriority = ranCustPriority });
 
            }

            // ASSERT
            Assert.Equal(15, customers.Count);
        }

        [Fact]
        public void TestHashSetFitMultipleCustomers()
        {
            // ARRANGE
            Customers customerOne = new Customers(5555, 2);
            Customers customerTwo = new Customers(444, 2);
            Customers customerThree = new Customers(2344, 2);
            Customers customerFour = new Customers(32344, 2);
            CustomerStorage customerStorage = new CustomerStorage();

            // ACT
            customerStorage.storeCustId(customerOne);
            customerStorage.storeCustId(customerTwo);
            customerStorage.storeCustId(customerThree);
            customerStorage.storeCustId(customerFour);

            // ASSERT
            Assert.Equal(4, customerStorage.custIdStorage.Count);
        }

    }
}