using System;
using System.ServiceModel;


namespace MMOPortal
{

    /*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 30/05/2018
References : TrueMarble Parcticle 05 submission.(Author: Oshan Mendis ) 
*/

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //Obtained from TrueMarble Practical 05 - Author:Oshan
                //TrueMarbleData -> Program.cs
                //(Accessed on 27th May 2018)

                NetTcpBinding ntb = new NetTcpBinding();
                ServiceHost sh = new ServiceHost(typeof(MMOPortalControllerImpl));

                sh.AddServiceEndpoint(typeof(IMMOPortalController), ntb, "net.tcp://localhost:50002/MMOPortal");

                sh.Open();

                System.Console.WriteLine("Press enter to close the portal");
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

            //End of code obtained from TrueMarble Practical 05

        }
    }
}
