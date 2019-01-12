﻿using System;
using System.ServiceModel;


/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 15/05/2018
References : TrueMarble Parcticle 05 submission.(Author: Oshan Mendis ) 
*/

namespace MMOData
{
    /*Part of this file comprises externally obtained code */
    class Program
    {

        static void Main(string[] args)
        {
            MMODataControllerImpl MMODCI = new MMODataControllerImpl();

            //Obtained from TrueMarble Practical 05
            //TrueMarbleData -> Program.cs
            //(Accessed on 15th May 2018)

            try
            {
                NetTcpBinding ntb = new NetTcpBinding();

                ServiceHost sh = new ServiceHost(MMODCI);

                sh.AddServiceEndpoint(typeof(IMMODataController), ntb, "net.tcp://localhost:56561/MMOData");

                sh.Open();
                System.Console.WriteLine("Press enter to close the server");
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
                Console.WriteLine("Error : Service host oobject is in either closed/closing/opened/opening state ");
            }

            //If open() / close() method fails - service host object is in a faulted state
            catch (CommunicationObjectFaultedException)
            {
                Console.WriteLine("Error : Server object is corrupted ");
            }

            //If open() / close() method fails - If defualt time allocated for the operation exceeds
            catch (TimeoutException)
            {
                Console.WriteLine("Error : Server timed out !");
            }

            //End of code obtained from TrueMarble Practical 05
        }
    }
}
