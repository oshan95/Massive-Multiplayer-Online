using System;
using System.Collections.Generic;
using System.ServiceModel;
using MMOBiz;

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 30/05/2018
*/

namespace MMOPortal
{
    //And single object of portal will be given to every client(in this case client is a business server)
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    internal class MMOPortalControllerImpl : IMMOPortalController
    {

        IMMOBizController game_func;
        IMMOBizController game;

        //IMMODataController game_data;
        List<string> servers = new List<string>();

        string MainServerUrl;

        public MMOPortalControllerImpl()
        {
            
        }

        //Adds a new server to the  server list of portal 
        public string AddNewServer(string url)
        {
            if (servers == null)
            {
                servers.Add(url);
                Console.WriteLine("New Server joined. Server ip address is : " + url);
                string Msg = "Information of server at " + url + " have been recorded. And will be opened for clients to connect!";
                return Msg;
            }
            else
            {
                foreach (string uri in servers)
                {
                    if (uri == url)
                    {
                        Console.WriteLine("Error: Trying to add a server already connected!");
                        return "error";
                    }
                }
                servers.Add(url);
                Console.WriteLine("New Server joined. Server ip address is : " + url);
                string Message = "Information of server at " + url + " have been recorded. And will be opened for clients to connect!";
                return Message;
            }
            
        }


        //To store the servers connected to the portal in a list
        public List<String> GetServerList()
        {
            return servers;
        }

        // user login validation
        public int LogInUser(int uid, string uname, string pwd)
        {
           
            int x = game_func.LogInUser(uid, uname, pwd);
            return x;
        }

        //This method is developed to connect with the first business server for user validation
        public void EstablishConn()
        {
            NetTcpBinding ntb = new NetTcpBinding();
            MainServerUrl = servers[0];

            try
            {
                ChannelFactory<IMMOBizController> MMOFactory = new ChannelFactory<IMMOBizController>(ntb, MainServerUrl);

                game_func = MMOFactory.CreateChannel();


            }

            //If URI is invalid or uri Cannot be parsed or empty uri
            catch (UriFormatException)
            {
                Console.WriteLine("Error : Uri format is not valid");
            }
            //If endpoint is null
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error : Null endpoint detected");
            }

            //If address is null
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error : Uri is not available");
            }

            //If address is null
            catch (EndpointNotFoundException)
            {
                Console.WriteLine("Error : Coulld not connect with the server. Please try again later!");
            }
        }

        //To send the list of users to the gui client to make sure the user trying to log actually exists 
        public List<string> GetUserList()
        {
            List<string> ul = null;
            try
            {
                EstablishConn();
                ul = game_func.GetUserList();
                return ul;
            }
            catch(EndpointNotFoundException)
            {
                Console.WriteLine("Could not communicate with the business server");
            }

            return ul;

        }

        //Gets particular user's name when the id is given
        public String GetUserNameById(int uid)
        {
            
            string name;
            name = game_func.GetUserNameById(uid);
            return name;
        }

        public void EstablishConnSec(string url)
        {
            NetTcpBinding ntb = new NetTcpBinding();

            try
            {
                ChannelFactory<IMMOBizController> MMOFactory = new ChannelFactory<IMMOBizController>(ntb, url);

                game = MMOFactory.CreateChannel();


            }

            //If URI is invalid or uri Cannot be parsed or empty uri
            catch (UriFormatException)
            {
                Console.WriteLine("Error : Uri format is not valid");
            }
            //If endpoint is null
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error : Null endpoint detected");
            }

            //If address is null
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error : Uri is not available");
            }

            //If address is null
            catch (EndpointNotFoundException)
            {
                Console.WriteLine("Error : Coulld not connect with the server. Please try again later!");
            }
        }

        //Retrieve the list of users logged in
        public List<int> GetUsersLoggedIn(string ServAddr)
        {
            if (ServAddr == MainServerUrl)
            {
                return game_func.GetUsersLoggedIn();
            }
            else
            {
                EstablishConnSec(ServAddr);
                return game.GetUsersLoggedIn();
            }
        }

        //Gets the friend lis of a particular user
        public List<string> GetFriendList(int uid)
        {
            List<string> friends = game_func.GetFriendList(uid);
            return friends;
        }

        
    }
}
