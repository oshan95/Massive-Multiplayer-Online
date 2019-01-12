using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using MMOPortal;

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 20/05/2018
References: 1) https://msdn.microsoft.com/en-us/library/ms131573(v=vs.110).aspx
            2) https://stackoverflow.com/questions/1546419/convert-file-path-to-a-file-uri
            3) https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c //Random string generation for unique uri
            4) TruMarble Practical 05 submission - Author: Oshan Mendis
*/

namespace MMOBiz
{

    /*Parts of this file comprises externally obtained code*/

    //This class initializes the multiple business servers
    class Program
    {
        static void Main(string[] args)
        {
            Random myrandom = new Random();            
       
            try
            {
                //Obtained from TrueMarble Practical 05
                //TrueMarbleBiz -> Program.cs
                //(Accessed on 19th May 2018)

                NetTcpBinding ntb = new NetTcpBinding();
                ServiceHost sh = new ServiceHost(typeof(MMOBizControllerImpl));

                //To create unique port number for the server to listen on 
                int PortNo = myrandom.Next(56562,59999);

                //Obtained from Microsoft Developer Network
                //https://msdn.microsoft.com/en-us/library/ms131573(v=vs.110).aspx
                //(Accessed on 20th May 2018)

                Uri BaseUri = new System.Uri("net.tcp://localhost:"+ PortNo + "/");
                Uri FinalUri;
                string UniqueString = GetUniqueKey(10);
                //Appends unique string to the base uri
                Uri.TryCreate(BaseUri, UniqueString, out FinalUri);

                //End of code obtained from Microsoft Developer Network

                string url = FinalUri.ToString();

                sh.AddServiceEndpoint(typeof(IMMOBizController), ntb, FinalUri);

                sh.Open();

                //To inform the server that a new business server is up and running 
                IMMOPortalController game_data;
                NetTcpBinding ntcb = new NetTcpBinding();
                ChannelFactory<IMMOPortalController> MMOFactory = new ChannelFactory<IMMOPortalController>(ntcb, "net.tcp://localhost:50002/MMOPortal");
                game_data = MMOFactory.CreateChannel();
                String msg = game_data.AddNewServer(url);

                //BizImpl.ExperimentalMethod();

                System.Console.WriteLine("Server ip : "+url);
                System.Console.WriteLine("Message from portal : " + msg);
                System.Console.WriteLine("Press enter to close the biz tier");
                System.Console.ReadLine();

                sh.Close();

            }
            //If could not add service endpoint(If binding is null or address is null )
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error : The endpoint address is invalid ");
            }

            //If open() / close() method fails - service host object is not in a opened or opening and modifiable state
            //If service host object is in a closed/closing and not modifiable state  
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error : Service host object is in either closed/closing/opened/opening state ");
            }

            //If open() / close() method fails - service host object is in a faulted state
            catch (CommunicationObjectFaultedException)
            {
                Console.WriteLine("Error : Service host object is corrupted ");
            }

            //If open() / close() method fails - If defualt time allocated for the operation exceeds
            catch (TimeoutException)
            {
                Console.WriteLine("Error : Biz tier program timed out !");
            }
            //If URI is invalid or uri Cannot be parsed or empty uri
            catch (UriFormatException)
            {
                Console.WriteLine("Error : The URI is invalid or corrupted");
            }
            //End of code obtained from TrueMarble Practical 05

        }

        //Obtained for StackOverFlow -> Author : Eric J
        //https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
        //(Accessed on 20th May 2018)

        //To create a unique string for server uri
        public static string GetUniqueKey(int maxSize)
        {

            try
            {
                char[] chars = new char[62];
                chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
                byte[] data = new byte[1];
                using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
                {
                    crypto.GetNonZeroBytes(data);
                    data = new byte[maxSize];
                    crypto.GetNonZeroBytes(data);
                }
                StringBuilder result = new StringBuilder(maxSize);
                foreach (byte b in data)
                {
                    result.Append(chars[b % (chars.Length)]);
                }
                return result.ToString();
            }

            //End of code obtained from StackOverFlow

            //If the cryptographic service cannot be run
            catch (CryptographicException)
            {
                Console.WriteLine("Error: Cryptographic service is not available");
            }
            //if input parameters are not included
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error: Input parameters are not valid or not found");
            }
            //If append function is not working
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error: Cannot append charactors to the string");
            }

            return null;
        }

    }
}
