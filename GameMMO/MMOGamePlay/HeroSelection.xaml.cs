using System;
using System.Collections.Generic;
using System.Windows;
using MMOBiz;

namespace MMOGamePlay
{

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 30/05/2018
*/

    /// <summary>
    /// Interaction logic for HeroSelection.xaml
    /// </summary>
    public partial class HeroSelection : Window
    {

        //To communicate with the business server
        IMMOBizController mmobiz;

        //To store the user id and name
        int UserId;
        string UserName;

        //To store the same friend list got in the previous window 
        List<string> Friends;

        //To stroe the server url
        String ServerUrl;

        //To store the server url and name
        SortedList<string, string> ServerNames = new SortedList<string, string>();

        //Assigned boss for the server
        int BossId;
        string BossName;

        public HeroSelection(IMMOBizController biz_obj, int uid, string uname, List<string> FriendList, string ServerAddressCon, SortedList<string, string> ServersAndNames)
        {
            mmobiz = biz_obj;

            BossId = mmobiz.GetBossId();
            BossName = mmobiz.GetBossName();

            UserId = uid;
            UserName = uname;
            Friends = FriendList;
            ServerUrl = ServerAddressCon;
            ServerNames = ServersAndNames;

            InitializeComponent();

            try
            {
                listHeros.ItemsSource = mmobiz.GetHeroNames();
            }
            catch
            {

            }

        }

        private void btnSelectHero(object sender, RoutedEventArgs e)
        {
            GamePlay gp = new GamePlay();
            gp.ShowDialog();
        }
    }
}
