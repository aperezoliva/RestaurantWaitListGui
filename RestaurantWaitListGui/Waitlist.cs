using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantWaitListGui
{
    public class Waitlist
    {
        private PriorityQueue<int, int> waitingQueue;
        public PriorityQueue<int, int> WaitingQueue { get => waitingQueue; set => waitingQueue = value; }
        public Waitlist()
        {
            this.WaitingQueue = new PriorityQueue<int, int>(1);
        }

        public void assignWaitList(Customers cust) // uses customer object to assign them to the waiting list
        {
            waitingQueue.Enqueue(cust.CustId, cust.CustPriority);
        }

        public int checkWaitListSize()
        {
            return waitingQueue.Count;
        }
    }
}
