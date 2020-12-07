using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using UnityEditor;
namespace BookiesT {



    public class SaveData : MonoBehaviour
    {
        public static SaveData instance;
        //[SerializeField] private UserData _userData = new UserData();
        [SerializeField] private BetData _betData = new BetData();

        [SerializeField] private TMP_Dropdown fightDescTMP;
        [SerializeField] private TMP_Dropdown fighterChoiceTMP;
        [SerializeField] public TextMeshProUGUI betAmountTMP;

        //[SerializeField] private TextMeshProUGUI temp;

        private void Awake()
        {
            instance = this;
        }
        public void MakeBetData()
        {
            UIManager.instance.OpenBetValidMenu();
            if (UIManager.instance.CheckPunterFunds())
            {
                
                _betData.betID = System.Guid.NewGuid().ToString();
                _betData.dateMade = System.DateTime.Now;
                _betData.fightDescription = UIManager.instance.fightTMP.options[UIManager.instance.fightTMP.value].text;
                _betData.fighterToWin = UIManager.instance.fighterTMP.options[UIManager.instance.fighterTMP.value].text;
                _betData.betAmount = UIManager.instance.betAmountTMP.text.ToString();
                _betData.punterName = UIManager.instance.punterTMP.options[UIManager.instance.punterTMP.value].text;
                _betData.fightWinner = "tbc";
                UIManager.instance.betValidText.text = string.Format("The bet has been placed\n\nThe user {0} has been deducted {1} T-bucks", _betData.punterName, _betData.betAmount);


                //SaveBetObject();
                SaveBetObjectJSON(_betData);
                //update user credit
                DataManager.Instance.UpdateUserCredit(_betData.betAmount);
                SaveUserCredit(DataManager.Instance.currentUser);


            }
            else
            {
                
                UIManager.instance.betValidText.text = "THERE IS NOT ENOUGH FUNDS FOR THIS USER TO MAKE A BET";
            }
        }

        private void AddMoney(int userID, float credit)
        {
            //REFERENCE USER ID IN DB

            //ADD CREDIT TO THAT AMOUNT
        }

      

        public void SaveBetObjectJSON(BetData betObj)
        {
            
               
                var jsonData = System.IO.File.ReadAllText("Assets/Resources/bookiesData.json");
                
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonData.ToString());

                DataTable dataTable = dataSet.Tables["bets"];

                DataRow row = dataTable.NewRow();

                row["betID"] = betObj.betID;
                row["dateMade"] = betObj.dateMade;
                row["fightDescription"] = betObj.fightDescription;
                row["fighterToWin"] = betObj.fighterToWin;
                row["betAmount"] = betObj.betAmount;
                row["punterName"] = betObj.punterName;
                row["fighterWon"] = betObj.fightWinner;
                dataTable.Rows.Add(row);

                string json = JsonConvert.SerializeObject(dataSet, Formatting.Indented);

                //Debug.Log(json.ToString());


                File.WriteAllText("Assets/Resources/bookiesData.json", json);

        }

        public void CreateUserObject(UserData userObj)
        {
            var jsonData = System.IO.File.ReadAllText("Assets/Resources/userData.json");

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonData.ToString());

            DataTable dataTable = dataSet.Tables["bets"];

            DataRow row = dataTable.NewRow();

            row["userID"] = userObj.userID;
            row["firstName"] = userObj.userName;
            row["credit"] = userObj.userCredit;
            row["betsMade"] = userObj.betsMade;
          
            dataTable.Rows.Add(row);

            string json = JsonConvert.SerializeObject(dataSet, Formatting.Indented);

            Debug.Log(json.ToString());


            File.WriteAllText("Assets/Resources/userData.json", json);
        }



        public void SaveUserCredit (UserData userObj)
        {
            //string jsonData = File.ReadAllText("Assets/Resources/userData.json");
            TextAsset fightDataJSON = Resources.Load<TextAsset>("userData");

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(fightDataJSON.ToString());

            DataTable dataTable = dataSet.Tables["users"];

            
            foreach (DataRow row in dataTable.Rows)
            {
                
                if (Convert.ToInt32(row["userID"]) == userObj.userID)
                {
                    Debug.Log("Match");
                    row["credit"] = userObj.userCredit;

                    break;
                }
                else
                {
                    Debug.Log("no find");
                }
            }

            dataSet.AcceptChanges();
            string output = JsonConvert.SerializeObject(dataSet, Newtonsoft.Json.Formatting.Indented);


             Debug.Log(output);
             File.WriteAllText("Assets/Resources/userData.json", output);
        }

    }






}




