using System;
using System.Collections.Generic;
using System.ServiceModel;
using MMOData;


/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 29/05/2018

References :  1. https://msdn.microsoft.com/en-us/library/system.random.next(v=vs.110).aspx
              2. https://stackoverflow.com/questions/10632776/fastest-way-to-remove-duplicate-value-from-a-list-by-lambda
              3. https://www.codeproject.com/Questions/725265/How-to-remove-duplicate-strings-of-list-string (Solution 6)
              4. https://stackoverflow.com/questions/7120522/how-to-store-a-pair-of-strings-in-a-non-unique-list
*/



namespace MMOBiz
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode =InstanceContextMode.Single, UseSynchronizationContext = false)]
    internal class MMOBizControllerImpl : IMMOBizController
    {
        //To keep track of number of users connected the server
        int NoOfPlayersConnected =0;

        IMMODataController game_data;

        //To store the user id of the user logged in
        List<int> UsersLoggedIn =  new List<int>();

        //To utilize the friend list
        Random myrandom = new Random();

        //This list stores heros selected by a particular user
        List<KeyValuePair<int, int>> HerosOfUsers = new List<KeyValuePair<int, int>>();

        //Hero usage stats
        SortedList<int, int> HeroUsageList = new SortedList<int, int>();

        int BossId;
        string BossName;


        public MMOBizControllerImpl()
        {
            NetTcpBinding ntb = new NetTcpBinding();

            try
            {
                ChannelFactory<IMMODataController> MMOFactory = new ChannelFactory<IMMODataController>(ntb, "net.tcp://localhost:56561/MMOData");

                game_data = MMOFactory.CreateChannel();

                System.Console.WriteLine("Successfully connected to the database !");

                //To initialize hero usage list
                InitHeroUsageList();

                //Assigning the one and only boss to the server 
                BossId = AssignABossToServer();
                BossName = game_data.GetBossNameByID(BossId);
            }

            //If URI is invalid or uri Cannot be parsed or empty uri
            catch (UriFormatException)
            {
                Console.WriteLine("Error : The URI is invalid or corrupted");
            }
            //If endpoint is null
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error : The method cannot be executed according to the object's current state ");
            }

            //If address is null
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error : Address of data tier could'nt be found");
            }
        }

        public void MessageFromPortal(string msg)
        {
            Console.WriteLine(msg);
        }



        //Main function for functionality of game
        public int StartGameRound()
        {
            if ( NoOfPlayersConnected > 4 && NoOfPlayersConnected < 13 )
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public List<string> GetHeroNames()
        {
            List<string> hl = new List<string>();
            foreach (KeyValuePair<int,int> item in HeroUsageList)
            {
                string name = game_data.GetHeroNameByID(item.Key);
                hl.Add(name);
            }
            return hl;
        }

        public void InitHeroUsageList()
        {
            int x = game_data.GetNumHeroes();

            for (int i = 0; i < x; i++)
            {
                HeroUsageList.Add(i, 0);
            }
        }

        //Gets the heroes that user selected
        public void AssignHeroToTheUser(int uid, int hid)
        {
            HeroCount(hid);
            HerosOfUsers.Add(new KeyValuePair<int, int>(uid, hid));

        }

        //To store number of time particular hero have been used in game
        public int HeroCount(int HeroId)
        {
            int key, val;

            foreach (KeyValuePair<int, int> item in HeroUsageList)
            {
                key = item.Key;
                val = item.Value;

                if (key == HeroId)
                {
                    HeroUsageList.Remove(key);
                    HeroUsageList.Add(key,val+1);
                    return 0;
                }
            }

            return -1;
        }

        //To get the number of time heros have been used in game
        public SortedList<string, int> GetHeroUsageStats()
        {

            SortedList<string, int> HeroCount = new SortedList<string, int>();

            foreach (KeyValuePair<int, int> item in HeroUsageList)
            {
                HeroCount.Add((game_data.GetHeroNameByID(item.Key)), item.Value);
            }

            return HeroCount;
        }


        //User log in
        public int LogInUser(int uid, string uname, string pwd)
        {
              
            String UserName = game_data.GetUsername(uid);
            String Password = game_data.GetUserPassword(uid);

            if (UserName == uname && Password == pwd)
            {
                return 0;
            }
            else
            {
                return -1;
            }

        }

       
        //Retruns the number of users connected the server
        public int NumOfUsersConnected()
        {
            return UsersLoggedIn.Count;
        }

        //Returns a utilized unique friend list for particular user
        public List<String> GetFriendList(int UserId)
        {

            int x = game_data.GetNumUsers();
            List<String> ufrens = game_data.GetFriendsByID(UserId);
            
            try
            {
                ufrens.Add(game_data.GetUsername(this.Rand(UserId)));
                ufrens.Add(game_data.GetUsername(this.Rand(UserId)));
                ufrens.Add(game_data.GetUsername(this.Rand(UserId)));
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("The specified user could not be found");

            }

            //Obtained for CodeProject -> Author : Praveen Nelge
            //https://www.codeproject.com/Questions/725265/How-to-remove-duplicate-strings-of-list-string
            //(Accessed on 18th May 2018)

            //Utilizing freind list of particular user
            Dictionary<string, int> UniqueList = new Dictionary<string, int>();
            List<string> UtilizedFriendList = new List<string>();

            try
            {
                foreach (string User in ufrens)
                {
                    try
                    {
                        if (!UniqueList.ContainsKey(User))
                        {
                            UniqueList.Add(User, 0);
                            UtilizedFriendList.Add(User);
                        }
                    }

             //End of code obtained from CodeProject

                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Dictionary does not contains the specified key");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Add method failed to the lists");
                    }

                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("The friend list is empty becuase the particular user cannot be found");
            }

            return UtilizedFriendList;

        }

        //To generate a non negative random number between zero and number of user count
        public int Rand(int uid)
        {
            int NoOfUsers = game_data.GetNumUsers() - 1;
            int x;
            int y = -1;

            try
            {
                 y = myrandom.Next(0, NoOfUsers);
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("Problem encountered with range to get a random value");
            }

            x = y;

            //To make sure not to return the id of the same user who fired the GetfreindList function
            //Checks if the generated random value is same as the logged in user's id. 
            if (x == uid)
            {
                return (myrandom.Next(0, NoOfUsers));
            }
            else
            {
                return x;
            }


        }


        //To get a list of user names
        public List<string> GetUserList()
        {
            List<String> ul = new List<string>();
            int NoOfUsers = 0;

            try
            {
                NoOfUsers = game_data.GetNumUsers();
            }
            catch(CommunicationException)
            {
                Console.WriteLine("Cannot get user count. Database connection is not provided");
            }

            for (int i = 0; i < NoOfUsers; i++)
            {
                ul.Add(game_data.GetUsername(i));
            }

            return ul;
        }

        //Adding random boss to the server from the boss list
        public int AssignABossToServer()
        {
            int MaxVal = game_data.GetNumBosses();
            int rand = myrandom.Next(MaxVal);
            return rand;
        }

        //To get list of id's of corresponding user
        public List<int> GetUsersLoggedIn()
        {
            return UsersLoggedIn;
        }

        //To get particualr user's user name by id
        public string GetUserNameById(int UserId)
        {
            string uname = null;
            uname = game_data.GetUsername(UserId);
            return uname;
        }

        //This method is run when new user is connects to the server
        public int AddUser(int uid)
        {
            if (UsersLoggedIn.Count < 12)
            {
                UsersLoggedIn.Add(uid);
                return 0;
            }
            else
            {
                return -1;
            }
            
        }

        //To get send the boss id to the gui
        public int GetBossId()
        {
            return BossId;
        }

        //To get send the boss id to the gui
        public string GetBossName()
        {
            return BossName;
        }

    }
}
