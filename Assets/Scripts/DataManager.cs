using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Linq;
using BookiesT;

namespace BookiesT {

    public class DataManager : MonoBehaviour
    {
        private static DataManager instance;

        public static DataManager Instance { get { return instance; } }

         public List<FightData> fights = new List<FightData>();
         public List<UserData> users = new List<UserData>();


         private List<string> fightList = new List<string>();
         private List<string> userList = new List<string>();
         public List<string> fighterList = new List<string>();
      
        

        int countLink = 0;
        string currentFightNameSelection;
        string currentUsernameSelection;

        int currentUserCredit;

        private void Awake()
        {
            instance = this;
        }
        
        private void Start()
        {
            ClearStart();
        }

        public void ClearStart()
        {
            ReadFightData();
            LoadFightData();
            LoadFighterData();

            ReadUserData();
            LoadUserData();
            GetUserCredit();
        }

        

        
        private void ReadFightData()
        {
            //read in csv of all fight data
            TextAsset fightData = Resources.Load<TextAsset>("fightdata");
            string[] data = fightData.text.Split(new char[] { '\n' });

            for (int i = 1; i < data.Length - 1; i++)
            {
                string[] row = data[i].Split(new char[] { ',' });

                if (row[1] != "")
                {
                    FightData f = new FightData();

                    int.TryParse(row[0], out f.fightID);
                    f.fightDescription = row[1];
                    f.fighterA = row[2];
                    f.fighterB = row[3];

                    fights.Add(f);
                }
            }
            //Debug.Log(fights.Count());
            //

        }
        private void ReadUserData()
        {

            //read in csv of all fight data
            TextAsset userData = Resources.Load<TextAsset>("punterdata");
            string[] data = userData.text.Split(new char[] { '\n' });


            for (int i = 1; i < data.Length - 1; i++)
            {

                string[] row = data[i].Split(new char[] { ',' });

                if (row[1] != "")
                {

                    UserData u = new UserData();

                    int.TryParse(row[0], out u.userID);
                    u.userName = row[1];
                    int.TryParse(row[2], out u.userCredit);
                    int.TryParse(row[3], out u.betsMade);

                    users.Add(u);

                }


            }
            //Debug.Log(users.Count());

            //

        }

        private void LoadFightData()
        {

            foreach (FightData f in fights)
            {
                //Debug.Log(f.fightDescription + "\nA:" + f.fighterA + "  B:" + f.fighterB);
                fightList.Add(f.fightDescription);

            }
            UIManager.instance.fightTMP.AddOptions(fightList);

        }
        private void LoadUserData()
        {

            foreach (UserData u in users)
            {
                //Debug.Log(f.fightDescription + "\nA:" + f.fighterA + "  B:" + f.fighterB);
                userList.Add(u.userName);

            }
            UIManager.instance.punterTMP.AddOptions(userList);

        }

        private void LoadFighterData()
        {
            UIManager.instance.fighterTMP.ClearOptions();
            fighterList.Clear();
            currentFightNameSelection = UIManager.instance.fightTMP.options[UIManager.instance.fightTMP.value].text;
            //Debug.Log(currentFightNameSelection);

            var match = fights.FirstOrDefault(f => f.fightDescription == currentFightNameSelection);

            fighterList.Add(match.fighterA);
            fighterList.Add(match.fighterB);


            UIManager.instance.fighterTMP.AddOptions(fighterList);
        }

        public void GetUserCredit()
        {
            currentUsernameSelection = UIManager.instance.punterTMP.options[UIManager.instance.punterTMP.value].text;

            var match = users.FirstOrDefault(u => u.userName == currentUsernameSelection);

            UIManager.instance.userLabel.text = match.userCredit.ToString();
            currentUserCredit = match.userCredit;

        }

        public void DropdownValueChanged(TMP_Dropdown change)
        {

            LoadFighterData();
        }



        

    }





}


