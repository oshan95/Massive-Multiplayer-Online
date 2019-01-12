using MMOBiz;
using MMOPortal;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;

namespace MMOGamePlay
{

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 30/05/2018
*/

    /// <summary>
    /// Interaction logic for ServerList.xaml
    /// </summary>
    /// 
    /*
     * References : 
     *              1) http://www.wpf-tutorial.com/listview-control/listview-gridview-left-aligned-column-names/
     *              2) https://stackoverflow.com/questions/44641000/wpf-listview-binding-a-sortedlist-containing-string-string
     *              3) https://www.youtube.com/watch?v=1lU-4VQpdRg
     *              4) https://forums.asp.net/t/1393697.aspx?SortedList+Get+Item+By+Index
     */
    public partial class ServerList : Window
    {
        IMMOBizController biz;
        IMMOPortalController portal;

        //To store the user id of the logged in user
        int UserId;
        string UserName;

        //To store the list od servers up and runnign
        List<string> Servers;

        //Used to assign names upto 9 servers
        string[] names = { "Asgard", "Hell's Kithcen", "Gotham", "Wakanda", "Krypton", "Sin City", "Utopia", "Central City", "Metropolis"};

        //List of server uri and their assigned names
        SortedList<string, string> ServersAndNames = new SortedList<string, string>();

        //To store the friend list of the user currently logged in
        List<string> FriendList;

        ChannelFactory<IMMOBizController> MMOFactory;
        NetTcpBinding ntb = new NetTcpBinding();

        public ServerList(int uid, List<string> sl, string uname, IMMOPortalController obj, List<string> UserFriends)
        {

            portal = obj;

            //getting user id, User name and server list from the login window and storing in here
            UserId = uid;
            UserName = uname;
            Servers = sl;
            FriendList = UserFriends;

            //Assigning names to servers in the server list
            AssignNamesToServers();
            InitializeComponent();

            //Setting the user name in the label
            SetUserName();
        }

        //Sets the username in the label in the gui
        public void SetUserName()
        {      
            lblUname.Content = "Welcome " +UserName;
        }


        //This fuction allows user to connect to a particular server
        private void Connect(object sender, RoutedEventArgs e)
        {
            try
            {
                //Checking if any connection is made already
                if (MMOFactory != null)
                {
                    //If a connection has already made checking if its state is opened or closing or faulted or opening
                    if (MMOFactory.State == CommunicationState.Opened || MMOFactory.State == CommunicationState.Closing || MMOFactory.State == CommunicationState.Faulted || MMOFactory.State == CommunicationState.Opening)
                    {
                        MMOFactory.Close();

                        //Gets the index of the address that user selected in the list view
                        int y = listServers.SelectedIndex;

                        //y==-1 becuase seceltedIndex returns -1 if the selection is empty
                        if (y == -1)
                        {
                            MessageBox.Show("Please select the row that contains server you wish to join and then click the button");
                        }
                        else
                        {
                            //Getting the address that user chose to connect
                            string ServerAddressCon = ServersAndNames.Values[y];


                            //Conecting with the business server that user selected
                            EstablishConnection(ServerAddressCon);

                            //adding the user id to the list at business server
                            //returns -1 if the server is at its max capacity
                            int x = biz.AddUser(UserId);

                            if (x == -1)
                            {
                                MessageBox.Show("The server is at its maximum capacity");
                            }
                            else
                            {
                                //Calling the default constructor of next window to be opened 
                                HeroSelection hs = new HeroSelection(biz, UserId, UserName, FriendList, ServerAddressCon, ServersAndNames);
                                hs.ShowDialog();
                            }

                        }
                    }
                    else
                    {
                        //Gets the index of the address that user selected in the list view
                        int y = listServers.SelectedIndex;

                        //y==-1 becuase seceltedIndex returns -1 if the selection is empty
                        if (y == -1)
                        {
                            MessageBox.Show("Please select the row that contains server you wish to join and then click the button");
                        }
                        else
                        {
                            //Getting the address that user chose to connect
                            string ServerAddressCon = ServersAndNames.Values[y];

                            //Conecting with the business server that user selected
                            EstablishConnection(ServerAddressCon);

                            //adding the user id to the list at business server
                            //returns -1 if the server is at its max capacity
                            int x = biz.AddUser(UserId);

                            if (x == -1)
                            {
                                MessageBox.Show("The server is at its maximum capacity");
                            }
                            else
                            {

                                //Calling the default constructor of next window to be opened 
                                HeroSelection hs = new HeroSelection(biz, UserId, UserName, FriendList, ServerAddressCon, ServersAndNames);
                                hs.ShowDialog();
                            }

                        }
                    }

                }
                else
                {
                    //Gets the index of the address that user selected in the list view
                    int y = listServers.SelectedIndex;

                    //y==-1 becuase seceltedIndex returns -1 if the selection is empty
                    if (y == -1)
                    {
                        MessageBox.Show("Please select the row that contains server you wish to join and then click the button");
                    }
                    else
                    {

                        //Getting the address that user chose to connect
                        string ServerAddressCon = ServersAndNames.Values[y];

                        //Conecting with the business server that user selected
                        EstablishConnection(ServerAddressCon);

                        //adding the user id to the list at business server
                        //returns -1 if the server is at its max capacity
                        int x = biz.AddUser(UserId);

                        if (x == -1)
                        {
                            MessageBox.Show("The server is at its maximum capacity");
                        }
                        else
                        {

                            //Calling the default constructor of next window to be opened 
                            HeroSelection hs = new HeroSelection(biz, UserId, UserName, FriendList, ServerAddressCon, ServersAndNames);
                            hs.ShowDialog();
                        }
                    }
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error : Server has stopped it's execution");
            }

            catch
            {
                MessageBox.Show("Error: Server is not responding");
            }

            
        }

        //Creates a connection with the desired server
        //For the use of connect function
        public void EstablishConnection(string ServAddr)
        {
            try
            {
                MMOFactory = new ChannelFactory<IMMOBizController>(ntb, ServAddr);

                biz = MMOFactory.CreateChannel();


            }

            //If URI is invalid or uri Cannot be parsed or empty uri
            catch (UriFormatException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
            //If endpoint is null
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }

            //If address is null
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }

            //If address is null
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error : Could not connect with the server. Please try again later!");
            }
        }

        //Returns friend lisft of an particular user
        private void GetFriends(object sender, RoutedEventArgs e)
        {

            int y = listServers.SelectedIndex;

            //y==-1 becuase seceltedIndex returns -1 if the selection is empty
            if (y == -1)
            {
                MessageBox.Show("Please select the row that contains server you wish to join and then click the button");
            }
            else
            {
                //getting the selected server address from the sorted list
                string ServerAddress = ServersAndNames.Values[y];

                List<int> users = portal.GetUsersLoggedIn(ServerAddress);

                if (users == null)
                {
                    MessageBox.Show("No Friends have connected to this server");
                }
                else
                {
                    //To store list of users logged into the particular server
                    List<string> NewUsers = new List<string>();

                    //This foreach eliminates user from seeing the user itself
                    foreach (int uid in users)
                    {
                        if (uid != UserId)
                        {
                            NewUsers.Add(portal.GetUserNameById(uid));
                        }
                    }

                    //To store the friends of particular user that connected into particular server 
                    List<string> FriendsInServer = new List<string>();

                    

                    //this comapres if their is any user in friend list also in the list of users logged in
                    foreach (String fname in FriendList)
                    {
                        foreach (string name in NewUsers)
                        {
                            if (fname == name)
                            {
                                FriendsInServer.Add(name);
                            }
                        }
                    }

                    ListFriends frens = new ListFriends(FriendsInServer);
                    frens.ShowDialog();

                }


            }

        }

        //This function assigns names to server using the string array list 'names' and saves the name and server url in a sorted list
        //Only 9 servers can be run becuase only 9 names are avaialable
        private void AssignNamesToServers()
        {
            int size = Servers.Count;

            for (int i = 0; i < size; i++)
            {
                ServersAndNames.Add(names[i], Servers[i]);
            }
        }

        //Displays the server list in the list view
        private void Server_List_Loaded(object sender, RoutedEventArgs e)
        {
            listServers.ItemsSource = ServersAndNames;
        }

        //To get a particular users friend list
        private void btnSeeFriends_Click(object sender, RoutedEventArgs e)
        {
            FriendList fl = new FriendList(FriendList);

            fl.ShowDialog();
        }
    }
}
