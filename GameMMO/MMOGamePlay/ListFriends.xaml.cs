using System;
using System.Collections.Generic;
using System.Windows;


namespace MMOGamePlay
{

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 30/05/2018
*/

    /// <summary>
    /// Interaction logic for ListFriends.xaml
    /// </summary>
    public partial class ListFriends : Window
    {
        
        public ListFriends(List<string> friends)
        {

            InitializeComponent();

            try
            {
                listFriends.ItemsSource = friends;
            }
            //If the list is empty
            catch (NullReferenceException)
            {
                MessageBox.Show("No friends have connected");
            }

        }

        
    }
}
