using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SaveData : MonoBehaviour
{
    //[SerializeField] private UserData _userData = new UserData();
    [SerializeField] private BetData _betData = new BetData();

    [SerializeField] private TextMeshProUGUI fightDescTMP;
    [SerializeField] private TextMeshProUGUI fighterChoiceTMP;
    [SerializeField] private TextMeshProUGUI betAmountTMP;
    [SerializeField] private TextMeshProUGUI punterTMP;

    //[SerializeField] private TextMeshProUGUI temp;


    private void MakeBetData()
    {
        _betData.betID = System.Guid.NewGuid().ToString();
        _betData.dateMade = System.DateTime.Now;
        _betData.fightDescription = fightDescTMP.text;
        _betData.fighterToWin = fighterChoiceTMP.text;
        _betData.betAmount = betAmountTMP.text;
        _betData.punterName = punterTMP.text;
        //_betData.winAmount = Convert.ToInt32();
    }



    private void AddMoney(int userID, float credit)
    {
        //REFERENCE USER ID IN DB

        //ADD CREDIT TO THAT AMOUNT
    }
}



[System.Serializable]
public class UserData
{
    public string userName;
    public int userID;
    public int userCredit;
    public int betsMade;

}

[System.Serializable]
public class BetData
{
    public string betID { get; set; }
    public string fightDescription { get; set; }
    public string fighterToWin { get; set; }
    public string betAmount { get; set; }
    public string punterName { get; set; }
    public DateTime dateMade { get; set; }
    //public int winAmount;

}
[System.Serializable]
public class FightData
{
    public int fightID;
    public string fightDescription;
    public string fighterA;
    public string fighterB;

}



