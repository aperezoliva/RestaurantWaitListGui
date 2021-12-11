using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantWaitListGui
{
    public class Restaurant : Waitlist
    {
        public List<int> restaurantList;
        private PriorityQueue<int, int> waitListQueue;

        public Restaurant()
        {
            this.restaurantList = new List<int>();
            this.waitListQueue = new PriorityQueue<int, int>();
        }

        public PriorityQueue<int, int> getWaitList(Waitlist waitList) // simply returns the waitlist's queue
        {
            waitListQueue = waitList.WaitingQueue;
            return waitListQueue;
        }

        public void customerEnter() // whenever method gets fired it should dequeue and add the custId at the top of the queue
        {
            int custId;
            custId = waitListQueue.Peek();
            restaurantList.Add(custId);
            waitListQueue.Dequeue();
        }

  
    }
}

