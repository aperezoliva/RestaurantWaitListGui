/**************************************************************
* Name        : Wait List GUi
* Author      : Alexander Perez Oliva
* Created     : 12/10/2021
* Course      : CIS 152 - Data Structures
* Version     : 1.0
* OS          : Windows 10
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : The final project, a waiting list gui which shows the amount of customers in line and the amount of customers in the restaurant
*               Input:  starting the program
*               Output: customerids sorted using an insertion sort, everything should be displayed to the user and have data structures working behind the scenes to get
*               customer out queue into the restauarant, then have them leave the restaurant, the restaurant should have a max capacity set in place and won't increase until someone leaves
* Academic Honesty: I attest that this is my original work.
* I have not used unauthorized source code, either modified or 
* unmodified. I have not given other fellow student(s) access to
* my program.         
***************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestaurantWaitListGui
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WaitListGui : Window
    {
        static CustomerStorage custStorage = new CustomerStorage();
        private Waitlist waitList = new Waitlist();
        private Restaurant restaurantOne = new Restaurant();
        private bool closeButtonWasClicked = false; // bool to check which buttons are clicked USED TO STOP A LOOP, IF NOT USED THE PROGRAM CRASHED WHILE RESTAURANT IS TRANSFERRING QUEUE TO LIST
        private int ranCustAmt;
        private int restaurantCap = 30; // cap of restaurant
        public List<int> list = new List<int>(custStorage.custIdStorage);

        public List<int> List { get => list; set => list = value; } // no matter WHAT i do  I can't make the listviewe display more than one item

        public WaitListGui()
        {
            InitializeComponent();
            
        }

        private async void openButton_Click(object sender, RoutedEventArgs e) //button handlers to open the restaurant and get the program going
        {
            attendeesLbl.Visibility = Visibility.Visible; // when the button to open the restaurant is clicked, open these buttons/labels
            inRestLabel.Visibility = Visibility.Visible; 
            btnCust.Visibility = Visibility.Visible;    
            lineLabel.Visibility = Visibility.Visible;
            lineTotal.Visibility = Visibility.Visible;
            restTotal.Visibility = Visibility.Visible;
            attendeesTotal.Visibility = Visibility.Visible;
            btnOpen.Visibility = Visibility.Collapsed;
            btnClose.Visibility = Visibility.Visible;


            closeButtonWasClicked = false;
            Random generateNum = new Random();
            int ranCustPriority = generateNum.Next(1, 2);
            int ranCustId = generateNum.Next(1001, 9999);
            ranCustAmt = generateNum.Next(10, 55);

            var customers = new List<Customers>();

            for (int i = 0; i < ranCustAmt; i++) // starts with customers at the beginning
            {
                customers.Add(new Customers { CustId = ranCustId, CustPriority = ranCustPriority });
                custStorage.storeCustId(customers[i]);
                
                waitList.assignWaitList(customers[i]);
            }



            list = custStorage.hashToList(list);
            attendeesTotal.Content = customers.Count.ToString();
            lineTotal.Content = waitList.WaitingQueue.Count.ToString();


            restaurantOne.getWaitList(waitList);


            for (int i = 0; i < ranCustAmt * 4; i++)
            {

                if (waitList.WaitingQueue.Count > 0 && !closeButtonWasClicked && restaurantOne.restaurantList.Count < restaurantCap)
                {

                    await Task.Delay(1000); // every 1 seconds a customer enters the restaurant until restaurant is full
                    restaurantOne.customerEnter();
                    lineTotal.Content = waitList.WaitingQueue.Count.ToString();
                    restTotal.Content = restaurantOne.restaurantList.Count.ToString();
                } else if (restaurantOne.restaurantList.Count == restaurantCap)
                {
              
                    restaurantOne.restaurantList.RemoveAt(0);
                    lineTotal.Content = waitList.WaitingQueue.Count.ToString();
                    restTotal.Content = restaurantOne.restaurantList.Count.ToString();
                }
                else if (closeButtonWasClicked)
                {
                    break;
                }
               
            }

            for (int i = 0; i < restaurantOne.restaurantList.Count; i++)
            {
                await Task.Delay(3000);
                restaurantOne.restaurantList.RemoveAt(i);
                restTotal.Content = restaurantOne.restaurantList.Count.ToString();
                i = -1; // My solution here is to basically RESTART i back to -1 then it goes up to 0, removing whatever is at 0
                            // Clunky but it works, earlier it was just removing and capping at 14 for some reason
            }
            restTotal.Content = restaurantOne.restaurantList.Count.ToString();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) // button to close
        {
            btnClose.Visibility = Visibility.Collapsed;
            attendeesLbl.Visibility = Visibility.Collapsed; // vice versa  of what happens above
            inRestLabel.Visibility = Visibility.Collapsed;
            lineLabel.Visibility = Visibility.Collapsed;
            btnOpen.Visibility = Visibility.Visible;
            btnCust.Visibility = Visibility.Collapsed;
            attendeesTotal.Visibility = Visibility.Collapsed;
            lineTotal.Visibility = Visibility.Collapsed;
            restTotal.Visibility = Visibility.Collapsed;

            closeButtonWasClicked = true;
            attendeesTotal.Content = 0;


            if (restaurantOne.restaurantList.Count != 0)
            {
                restaurantOne.restaurantList.Clear();
            }


            if (waitList.WaitingQueue.Count != 0)
            {
                waitList.WaitingQueue.Clear();
            }
              
        }

        private void custButton_Click(object sender, RoutedEventArgs e) // opens the cust list
        {
            btnClose.Visibility = Visibility.Collapsed;
            attendeesLbl.Visibility = Visibility.Collapsed; 
            inRestLabel.Visibility = Visibility.Collapsed;
            lineLabel.Visibility = Visibility.Collapsed;
            btnOpen.Visibility = Visibility.Collapsed;
            btnCust.Visibility = Visibility.Collapsed;
            attendeesTotal.Visibility = Visibility.Collapsed;
            lineTotal.Visibility = Visibility.Collapsed;
            restTotal.Visibility = Visibility.Collapsed;
            btnCustClose.Visibility = Visibility.Visible;
            custList.Visibility = Visibility.Visible;



            InsertionSort(list);

            custList.ItemsSource = list;
        }

        private void custButtonClose_Click(object sender, RoutedEventArgs e)
        {
            attendeesLbl.Visibility = Visibility.Visible; 
            inRestLabel.Visibility = Visibility.Visible;
            btnCust.Visibility = Visibility.Visible;
            lineLabel.Visibility = Visibility.Visible;
            lineTotal.Visibility = Visibility.Visible;
            restTotal.Visibility = Visibility.Visible;
            attendeesTotal.Visibility = Visibility.Visible;
            btnOpen.Visibility = Visibility.Collapsed;
            btnClose.Visibility = Visibility.Visible;
            custList.Visibility = Visibility.Collapsed;
            btnCustClose.Visibility= Visibility.Collapsed;

        }

        public static void InsertionSort(List<int> listInput) // Decided to take this from an earlier project, I wanted to sort the list I have provided above
        {

            for (int i = 0; i < listInput.Count; i++)
            {
                int item = listInput[i];
                int currentIndex = i;

                while (currentIndex > 0 && listInput[currentIndex - 1] > item)
                {
                    listInput[currentIndex] = listInput[currentIndex - 1];
                    currentIndex--;
                }

                listInput[currentIndex] = item;
            }
        }
    }
}
