using System;
using System.Collections.Generic;
using System.Windows;
using MMOPortal;
using System.ServiceModel;

namespace MMOGamePlay
{

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 30/05/2018
*/

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /*References : 1) https://stackoverflow.com/questions/3100837/set-background-image-on-grid-in-wpf-using-c-sharp/3100863
     *             2) https://stackoverflow.com/questions/5315497/how-to-center-a-label-text-in-wpf
     *             3) https://www.reddit.com/r/learnprogramming/comments/2y1sdm/c_wpf_passwordboxpassword_compared_with/
     */

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public partial class MainWindow : Window
    {
        //Id and name of the user successfully logged in
        int UserId;
        string UserName;

        //To store the friend list of the user currently logged in
        List<string> FriendList;

        IMMOPortalController Portal;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NetTcpBinding ntb = new NetTcpBinding();

            try
            {
                ChannelFactory<IMMOPortalController> MMOFactory = new ChannelFactory<IMMOPortalController>( ntb, "net.tcp://localhost:50002/MMOPortal");

                Portal = MMOFactory.CreateChannel();
            }

            //If URI is invalid or uri Cannot be parsed or empty uri
            catch (UriFormatException ex)
            {
                MessageBox.Show("Error : Cannot connet with the portal ");
            }
            //If endpoint is null
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error : Cannot connet with the portal ");
            }

            //If address is null
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Error : Cannot connet with the portal ");
            }

            //If address is null
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error : Could not connect with the server. Please try again later!");
            }

            
        }

        //This function is executed when the login button is clicked
        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtBoxUname.Text.Trim()) && !String.IsNullOrWhiteSpace(txtBoxPwd.Password.Trim()))
                {
                    string uname = txtBoxUname.Text;
                    string password = txtBoxPwd.Password;
                    List<string> ulist = ulist = Portal.GetUserList(); ;

                    int id = ulist.IndexOf(uname);

                    int x = Portal.LogInUser(id, uname, password);
                    FriendList = Portal.GetFriendList(UserId);

                    if (x == 0)
                    {
                        //Saving the user id if user successully logs in
                        UserId = id;
                        UserName = Portal.GetUserNameById(UserId);

                        ServerList ServerWindow = new ServerList(UserId, Portal.GetServerList(), UserName, Portal, FriendList);
                        ServerWindow.ShowDialog();


                    }
                    else
                    {
                        MessageBox.Show("Incorrect username or password");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter both password and user name ! ");
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error : Cannot communicate with the portal");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Error : Cannot communicate with the portal");
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Error : Cannot open the next window");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Error : Invalid data memebers found");
            }
            
        }

    }
}
