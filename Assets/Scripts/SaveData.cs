using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;


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
            _betData.betID = System.Guid.NewGuid().ToString();
            _betData.dateMade = System.DateTime.Now;
            _betData.fightDescription = UIManager.instance.fightTMP.options[UIManager.instance.fightTMP.value].text;
            _betData.fighterToWin = UIManager.instance.fighterTMP.options[UIManager.instance.fighterTMP.value].text;
            _betData.betAmount = UIManager.instance.betAmountTMP.text.ToString();
            _betData.punterName = UIManager.instance.punterTMP.options[UIManager.instance.punterTMP.value].text;
            _betData.fightWinner = "tbc";

            //_betData.winAmount = Convert.ToInt32();
            Debug.Log(betAmountTMP.text);
            SaveIntoJson();
        }

        private void AddMoney(int userID, float credit)
        {
            //REFERENCE USER ID IN DB

            //ADD CREDIT TO THAT AMOUNT
        }

        public void SaveIntoJson()
        {

            // Debug.Log(_betData.betID);
            string betData = JsonUtility.ToJson(_betData);




            var csv = new StringBuilder();


            //TODO
            //figure out why adding a new line to record
            var newLine = string.Format(@"{0},{1},{2},{3},{4},{5},{6}",
                _betData.betID, _betData.dateMade, _betData.fightDescription, _betData.fighterToWin, _betData.betAmount, _betData.punterName, _betData.fightWinner).Replace("\n", "").Replace("\r", "");
            csv.AppendLine(newLine);

            Debug.Log(newLine);

            Debug.Log(csv.ToString());
            System.IO.File.AppendAllText(Application.persistentDataPath + "/BookiesData.csv", csv.ToString());
        }


    }






}




