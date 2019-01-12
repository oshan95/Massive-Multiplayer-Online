using System.Collections.Generic;
using System.ServiceModel;

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 18/05/2018
*/

namespace MMOData
{
    //Biz server intercats with this interface
    [ServiceContract]
    public interface IMMODataController
    {

        [OperationContract]
        string GetUsername(int id);

        [OperationContract]
        string GetUserPassword(int id);

        [OperationContract]
        List<string> GetFriendsByID(int id);

        [OperationContract]
        int GetNumHeroes();

        [OperationContract]
        int GetNumBosses();

        [OperationContract]
        int GetNumUsers();

        [OperationContract]
        string GetHeroNameByID(int id);

        [OperationContract]
        string GetBossNameByID(int id);

        [OperationContract]
        int GetHeroDef(int id);

        [OperationContract]
        int GetHeroHp(int id);

        [OperationContract]
        int GetHeroMoveNum(int id);

        [OperationContract]
        int GetBossDef(int id);

        [OperationContract]
        int GetBossHp(int id);

        [OperationContract]
        int GetBossDamage(int id);

        [OperationContract]
        char GetBossTargetPref(int id);

        [OperationContract]
        int GetHeroAbilityVal(int id, int index);

        [OperationContract]
        string GetHeroAbilityDescription(int id, int index);

        [OperationContract]
        char GetHeroAbilityType(int id, int index);

        [OperationContract]
        char GetHeroAbilityTarget(int id, int index);

    }
}
