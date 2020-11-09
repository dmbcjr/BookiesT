using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;

public class SaveData : MonoBehaviour
{
    //[SerializeField] private UserData _userData = new UserData();
    [SerializeField] private BetData _betData = new BetData();

    [SerializeField] private TMP_Dropdown fightDescTMP;
    [SerializeField] private TMP_Dropdown fighterChoiceTMP;
    [SerializeField] private TextMeshProUGUI betAmountTMP;
    [SerializeField] private TMP_Dropdown punterTMP;

    //[SerializeField] private TextMeshProUGUI temp;


    public void MakeBetData()
    {
        _betData.betID = System.Guid.NewGuid().ToString();
        _betData.dateMade = System.DateTime.Now;
        _betData.fightDescription = fightDescTMP.options[fightDescTMP.value].text;
        _betData.fighterToWin = fighterChoiceTMP.options[fighterChoiceTMP.value].text;
        _betData.betAmount = betAmountTMP.text.ToString();
        _betData.punterName = punterTMP.options[punterTMP.value].text;
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

        var newLine = string.Format(@"{0},{1},{2},{3},{4},{5},{6}",
            _betData.betID, _betData.dateMade, _betData.fightDescription, _betData.fighterToWin, _betData.betAmount, _betData.punterName,_betData.fightWinner);
        csv.AppendLine(newLine);

        Debug.Log(newLine);

        Debug.Log(csv.ToString());
        System.IO.File.AppendAllText(Application.persistentDataPath + "/BookiesData.csv", csv.ToString());
    }


}






