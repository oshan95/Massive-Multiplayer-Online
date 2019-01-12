using System;
using System.Collections.Generic;
using System.ServiceModel;

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 30/05/2018 
*/

namespace MMOPortal
{
    [ServiceContract]
    public interface IMMOPortalController
    {
        [OperationContract]
        String AddNewServer(string url);

        [OperationContract]
        List<String> GetServerList();

        [OperationContract]
        int LogInUser(int uid, string uname, string pwd);

        [OperationContract]
        List<string> GetUserList();

        [OperationContract]
        string GetUserNameById(int uid);

        [OperationContract]
        List<int> GetUsersLoggedIn(string ServAddr);

        [OperationContract]
        List<string> GetFriendList(int uid);
    }
}
