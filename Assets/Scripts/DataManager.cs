using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Linq;
using BookiesT;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using UnityEditor;
using System;

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


        [SerializeField] public UserData currentUser = new UserData();


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
           // ClearStart();
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
            TextAsset fightDataJSON = Resources.Load<TextAsset>("fightDataJS");

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(fightDataJSON.ToString());

            DataTable dataTable = dataSet.Tables["fights"];

            foreach (DataRow row in dataTable.Rows)
            {
                FightData f = new FightData();

                f.fightID = System.Convert.ToInt32(row["fightID"]);
                f.fightDescription = (string)row["fightDescription"];
                f.fighterA = (string)row["oppoA"];
                f.fighterB = (string)row["oppoB"];

                fights.Add(f);


            }
            
        }

        private void ReadUserData()
        {
            TextAsset userDataJSON = Resources.Load<TextAsset>("userData");

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(userDataJSON.ToString());

            DataTable dataTable = dataSet.Tables["users"];

            foreach (DataRow row in dataTable.Rows)
            {
                UserData u = new UserData();

                u.userID = System.Convert.ToInt32(row["userID"]);
                u.userName = (string)row["firstName"];
                u.userCredit = System.Convert.ToInt32(row["credit"]);
                u.betsMade = System.Convert.ToInt32(row["betsMade"]);

                users.Add(u);

            }

            
        }

        public void APRESSMEE()
        {
            //UpdateUserCredit(10000);
           
            //Debug.Log(currentUser.userCredit);
            SaveData.instance.SaveUserCredit(currentUser);
        }
        private void LoadUserData()
        {

            foreach (UserData u in users)
            {
                
                userList.Add(u.userName);

            }
            UIManager.instance.punterTMP.AddOptions(userList);

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
      
        public void LoadFighterData()
        {
            UIManager.instance.fighterTMP.ClearOptions();
            fighterList.Clear();
            currentFightNameSelection = UIManager.instance.fightTMP.options[UIManager.instance.fightTMP.value].text;
            

            var match = fights.FirstOrDefault(f => f.fightDescription == currentFightNameSelection);

            
            fighterList.Add(match.fighterA);
            fighterList.Add(match.fighterB);


            UIManager.instance.fighterTMP.AddOptions(fighterList);
        }

        public void GetUserCredit()
        {
            currentUsernameSelection = UIManager.instance.punterTMP.options[UIManager.instance.punterTMP.value].text;


            var match = users.FirstOrDefault(u => u.userName == currentUsernameSelection);


            
            UIManager.instance.fundsText.text = match.userCredit.ToString();
            currentUserCredit = match.userCredit;

        }

        public void UpdateUserCredit(string creditToSubtract)
        {

            currentUsernameSelection = UIManager.instance.punterTMP.options[UIManager.instance.punterTMP.value].text;

            currentUser = users.FirstOrDefault(u => u.userName == currentUsernameSelection);

            currentUser.userCredit -= Convert.ToInt32(creditToSubtract);

           
        }
        
        
        public void DropdownValueChanged(TMP_Dropdown change)
        {

            LoadFighterData();
        }


    }





}


