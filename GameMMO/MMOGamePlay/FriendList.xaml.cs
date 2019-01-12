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
    /// Interaction logic for FriendList.xaml
    /// </summary>
    /// 

    public partial class FriendList : Window
    {
        public FriendList(List<string> friends)
        {
            InitializeComponent();


            try
            {
                ListOfFriends.ItemsSource = friends;
            }
            //If the list is empty
            catch (NullReferenceException)
            {
                MessageBox.Show("No friends have connected");
            }
        }
    }
}
