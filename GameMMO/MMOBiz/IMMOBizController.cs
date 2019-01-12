using System;
using System.Collections.Generic;
using System.ServiceModel;

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 29/05/2018
*/

namespace MMOBiz
{
    //Interface for client calls
    //Function calls from clients initially forwarded to this class. 
    //Then the call is directed to the MMOBizControllerImpl class(Where the actual function is implemented).

    [ServiceContract]
    public interface IMMOBizController
    {
        [OperationContract]
        int StartGameRound();

        [OperationContract]
        SortedList<string,int> GetHeroUsageStats();

        [OperationContract]
        int HeroCount(int HeroId);

        [OperationContract]
        List<String> GetFriendList(int UserId);

        [OperationContract]
        int NumOfUsersConnected();

        [OperationContract]
        void MessageFromPortal(string msg);

        [OperationContract]
        int LogInUser(int uid, string uname, string pwd);

        [OperationContract]
        List<string> GetUserList();

        [OperationContract]
        List<int> GetUsersLoggedIn();

        [OperationContract]
        string GetUserNameById(int UserId);

        [OperationContract]
        int AddUser(int uid);

        [OperationContract]
        int GetBossId();

        [OperationContract]
        string GetBossName();

        [OperationContract]
        List<string> GetHeroNames();

    }
}
