using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using DistributedGameDatabase;


/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 18/05/2018
*/

namespace MMOData
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]

    //This class contains functions that can only be called by the business serevr
    internal class MMODataControllerImpl : IMMODataController
    {
        //To check if the database was initialized successfully return value of the dbInit function is imported from the Program.cs
        int dbInit;

        DistributedGameDB DataBase = new DistributedGameDB();

        //Constructor method
        public MMODataControllerImpl()
        {


            try
            {
                dbInit = DataBase.InitDB();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not found the " + "highly_secure_user_file.txt");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("The User List is corrupted");
            }
            


        }

        //Return boss's damage value by passing particular boss's id
        public int GetBossDamage(int id)
        {
            //To return -1 if the database is not initialized
            int dmg = -1;

            int def, hp;
            char targetPref;

            if (dbInit == 0)
            {

                try
                {
                    DataBase.GetBossStatsByID(id, out def, out hp, out dmg, out targetPref);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return dmg;

        }

        //Return boss's defence value by passing particular boss's id
        public int GetBossDef(int id)
        {
            
            //To return -1 if the database is not initialized
            int def = -1;

            int dmg, hp;
            char targetPref;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetBossStatsByID(id, out def, out hp, out dmg, out targetPref);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return def;

        }

        //Return boss's hp by passing particular boss's id
        public int GetBossHp(int id)
        {
            //To return -1 if the database is not initialized
            int hp = -1;

            int dmg, def;
            char targetPref;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetBossStatsByID(id, out def, out hp, out dmg, out targetPref);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return hp;

        }

        //Returns boss's name by passing particular boss's id
        public string GetBossNameByID(int id)
        {
            //To return null string if the database is not initialized
            string name = null;

            if (dbInit == 0)
            {
                try
                {
                    name = DataBase.GetBossNameByID(id);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return name;

        }

        //Retruns Boss's target preference by passing particular boss's id
        public char GetBossTargetPref(int id)
        {
            //To return a empty character if the databse is not initialized
            char TargetPref = ' ';

            int dmg, def, hp;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetBossStatsByID(id, out def, out hp, out dmg, out TargetPref);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return TargetPref;

        }

        //Returns list of friends of a particular player by passing player's id
        public List<string> GetFriendsByID(int id)
        {
            //To return a null list if the databse is not initialized
            List<string> lst = null;

            if (dbInit == 0)
            {
                try
                {
                        lst = DataBase.GetFriendsByID(id);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not found the specified user because user list is not available");
                }

            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return lst;

        }

        //returns hero's ability description by passing the particualr hero's id & index number
        public string GetHeroAbilityDescription(int id, int index)
        {
            //To return null string if the database is not initialized
            string description = null;

            int value;
            char type, target;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetMovesByIDAndIndex(id, index, out value, out description, out type, out target);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return description;
        }

        //returns hero's ability target by passing the particualr hero's id & index number
        public char GetHeroAbilityTarget(int id, int index)
        {
            //To return a empty character if the databse is not initialized
            char target = ' ';

            string description;
            int value;
            char type;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetMovesByIDAndIndex(id, index, out value, out description, out type, out target);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return target;
        }

        //returns hero's ability type by passing the particualr hero's id & index number
        public char GetHeroAbilityType(int id, int index)
        {
            //To return a empty character if the databse is not initialized
            char type = ' ';

            string description;
            int value;
            char target;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetMovesByIDAndIndex(id, index, out value, out description, out type, out target);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return type;
        }

        //returns hero's ability value by passing the particualr hero's id & index number
        public int GetHeroAbilityVal(int id, int index)
        {
            //To return -1 if the database is not initialized
            int value = -1;
            string description;
            char target, type;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetMovesByIDAndIndex(id, index, out value, out description, out type, out target);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return value;
        }

        //returns hero's number of defences by passing the particualr hero's id
        public int GetHeroDef(int id)
        {
            //To return -1 if the database is not initialized yet
            int def = -1;

            int hp, MoveNum;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetHeroStatsByID(id, out def, out hp, out MoveNum);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return def;

        }

        //returns hero's hp by passing the particualr hero's id
        public int GetHeroHp(int id)
        {
            //To return -1 if the database is not initialized
            int hp = -1;

            int moveNum, def;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetHeroStatsByID(id, out def, out hp, out moveNum);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return hp;

        }

        //returns hero's move number by passing the particualr hero's id
        public int GetHeroMoveNum(int id)
        {
            //To return -1 if the database is not initialized
            int moveNum = -1;

            int hp, def;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetHeroStatsByID(id, out def, out hp, out moveNum);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return moveNum;
        }

        //Returns particualr hero's name by passing the hero's id
        public string GetHeroNameByID(int id)
        {
        
            //To return null string if the database is not initialized
            string name = null;

            if (dbInit == 0)
            {
                try
                {
                    name = DataBase.GetHeroNameByID(id);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return name;

        }

        //Retruns number of bosses of the game
        public int GetNumBosses()
        {
            //To return -1 if the database is not initialized
            int count = -1;

            if (dbInit == 0)
            {
                try
                {
                    count = DataBase.GetNumBosses();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return count;

        }

        //returns number of heroes of the game
        public int GetNumHeroes()
        {
            //To return -1 if the database is not initialized
            int count = -1;

            if (dbInit == 0)
            {
                try
                {
                    count = DataBase.GetNumHeroes();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not access data because database cannot be initialized");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return count;

        }

        //Returns number of user of the game
        public int GetNumUsers()
        {
   
            //To return -1 if the database is not initialized
            int count = -1;

            if (dbInit == 0)
            {
                try
                {
                    count = DataBase.GetNumUsers();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not found the number of users because user list is not available");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return count;

        }

        //Returns particualr user's password by passing the user id
        public string GetUserPassword(int id)
        {
            //To return null string if the database is not initialized
            string password = null;

            string UserName;

            if (dbInit == 0)
            {
                
                try
                {
                    DataBase.GetUsernamePassword(id, out UserName, out password);
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Could not found the specified user because user list is not available");
                }
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return password;

        }

        //Returns particular user's user name by passing the user id
        public string GetUsername(int id)
        {
            
            //Returns null string if the database have not initialized yet
            string UserName = null;

            string password;

            if (dbInit == 0)
            {
                try
                {
                    DataBase.GetUsernamePassword(id, out UserName, out password);
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("The specififed user does not exist ! ");
                }
                
            }
            else
            {
                System.Console.WriteLine("Request for function detected even the databse is not initialized yet");
            }

            return UserName;

        }
    }
}
